using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using Microsoft.Owin.Security;
using System.Security.Claims;
using AutoMapper;
using BLL.DTO;
using WEB.Models;

namespace WEB.Controllers
{
    public class AccountController : Controller
    {
        private IIdentityServices identityServices;

        public AccountController(IIdentityServices identityServices)
        {
            this.identityServices = identityServices;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Login(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
              UserDTO user =  identityServices.GetAllUsers().FirstOrDefault(p => p.Email == model.Email && p.Password == model.Password); ;
               
                if (user == null)
                {
                    ModelState.AddModelError("", "Неверный логин или пароль.");
                }
                else
                {
                    ClaimsIdentity claim = new ClaimsIdentity("ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    claim.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.String));
                    claim.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email, ClaimValueTypes.String));
                    claim.AddClaim(new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider",
                        "OWIN Provider", ClaimValueTypes.String));
                    if (user.Role != null)
                        claim.AddClaim(new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name, ClaimValueTypes.String));

                    AuthenticationManager.SignOut();
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);
                    return RedirectToAction("ListNews", "Admin");
                }
            }
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserViewModel userViewModel = new UserViewModel
                {
                    Email = model.Email,
                    Password = model.Password
                };

                Mapper.Initialize(cfg => cfg.CreateMap<UserDTO, UserViewModel>().ReverseMap());
                UserDTO user = Mapper.Map<UserViewModel,UserDTO >(userViewModel);

                identityServices.SetUser(user);

                return RedirectToAction("ListNews", "Admin"); 
            }

            // Появление этого сообщения означает наличие ошибки; повторное отображение формы
            return RedirectToAction("Index", "Home");
        }
    }
}