using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Interfaces;
using AutoMapper;
using BLL.DTO;
using WEB.Models;
using System.Data.Entity;
using System.IO;
using DAL.Entities;


namespace WEB.Controllers
{
    [ValidateInput(false)]
    public class AdminController : Controller
    {
        INewsService newsService;

        public AdminController(INewsService newsService)
        {
            this.newsService = newsService;
        }
        // GET: Admin
        public ActionResult ListNews(string NameRubric, string NameSourceNews, string NameDataCreate, int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            IEnumerable<NewsDTO> newsDtos = newsService.GetAllNews();

            Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>()
                .ForMember(d => d.RubricName, opt => opt.MapFrom(src => src.Rubric.NameRubric))
                .ForMember(d => d.SourceName, opt => opt.MapFrom(src => src.NewsSource.NameSourceNews)));


            var news = Mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos);

            IEnumerable<NewsViewModel> newsView = news;

            if (!String.IsNullOrEmpty(NameRubric) && !NameRubric.Equals("Все"))
            {
                newsView = newsView.Where(p => p.RubricName == NameRubric);
            }

            if (!String.IsNullOrEmpty(NameSourceNews) && !NameSourceNews.Equals("Все"))
            {
                newsView = newsView.Where(p => p.SourceName == NameSourceNews);
            }

            if (!String.IsNullOrEmpty(NameDataCreate) && !NameDataCreate.Equals("Новые"))
            {
                newsView = newsView.OrderByDescending(p => p.Id);
            }
            SelectList rubrics = new SelectList(newsService.GetAllRubrics(), "NameRubric", "NameRubric");

            SelectList suorceNews = new SelectList(newsService.GetAllNewsSources(), "NameSourceNews", "NameSourceNews");
            ViewBag.Rublics = rubrics;
            ViewBag.SourceNews = suorceNews;
            NewsRubricViewModel newsRubricViewModel = new NewsRubricViewModel()
            {
                NewsViewModel = newsView
            };
            return View(newsRubricViewModel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            
            SelectList rubrics = new SelectList(newsService.GetAllRubrics(), "Id", "NameRubric");
            SelectList suorceNews = new SelectList(newsService.GetAllNewsSources(), "Id", "NameSourceNews");
            ViewBag.Rublics = rubrics;
            ViewBag.SourceNews = suorceNews;
            return View();
        }

        [HttpPost]
        public ActionResult Create(NewsViewModel news)
        {
            news.CreateDateTimeNews = DateTime.Now;

            Mapper.Initialize(cfr => cfr.CreateMap<NewsDTO, NewsViewModel>()
                .ReverseMap());

            NewsDTO newsDTO = Mapper.Map<NewsViewModel, NewsDTO>(news);
            newsService.SetNews(newsDTO);

            return RedirectToAction("ListNews");
        }

        [HttpPost]
        public JsonResult UploadAjax(string bgSrc)
        {
            string image = bgSrc.Substring(22);
            var myfilename = string.Format(@"{0}", Guid.NewGuid());
            string filepath = "~/Images/" + myfilename + ".jpeg";
            var bytess = Convert.FromBase64String(image);
            using (var imageFile = new FileStream(Server.MapPath(filepath), FileMode.Create))
            {
                imageFile.Write(bytess, 0, bytess.Length);
                imageFile.Flush();
            }

            return Json(filepath.Substring(1));
        }
    }
}