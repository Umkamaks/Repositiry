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
using PagedList;

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

            IEnumerable<NewsViewModel> newsView = news.OrderByDescending(p=>p.Id);

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
                newsView = newsView.OrderBy(p => p.Id);
            }
            SelectList rubrics = new SelectList(newsService.GetAllRubrics(), "NameRubric", "NameRubric");

            SelectList suorceNews = new SelectList(newsService.GetAllNewsSources(), "NameSourceNews", "NameSourceNews");
            ViewBag.Rublics = rubrics;
            ViewBag.SourceNews = suorceNews;
            ViewBag.News = newsView.ToPagedList(pageNumber, pageSize);
            return View();
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            NewsDTO newsDto = newsService.GetNews(id);

            if (newsDto != null)
            {
                SelectList rubrics = new SelectList(newsService.GetAllRubrics(), "Id", "NameRubric");
                SelectList suorceNews = new SelectList(newsService.GetAllNewsSources(), "Id", "NameSourceNews");

                ViewBag.Rublics = rubrics;
                ViewBag.SourceNews = suorceNews;
                Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>());

                NewsViewModel news = Mapper.Map<NewsDTO, NewsViewModel>(newsDto);

                return View(news);
            }
            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult Edit(NewsViewModel news)
        {
            news.CreateDateTimeNews = DateTime.Now;
            Mapper.Initialize(cfg => cfg.CreateMap<NewsViewModel, NewsDTO>()
            .ReverseMap());

            NewsDTO newsDTO = Mapper.Map<NewsViewModel, NewsDTO>(news);
            newsService.UpdataNews(newsDTO);
            return RedirectToAction("ListNews");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            NewsDTO newsDto = newsService.GetNews(id);
            if (newsDto == null)
            {
                return HttpNotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>());

            NewsViewModel news = Mapper.Map<NewsDTO, NewsViewModel>(newsDto);
            return View(news);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            NewsDTO newsDto = newsService.GetNews(id);
            if (newsDto == null)
            {
                return HttpNotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>());

            NewsViewModel news = Mapper.Map<NewsDTO, NewsViewModel>(newsDto);

            newsService.DeleteNews(id.Value);
            return RedirectToAction("ListNews");
        }

        //работа с рубриками
        public ActionResult RubricList()
        {

            IEnumerable<RubricDTO> rubric = newsService.GetAllRubrics();
            Mapper.Initialize(cfg => cfg.CreateMap<RubricDTO, RubricViewModel>());

            IEnumerable<RubricViewModel> news = Mapper.Map<IEnumerable<RubricDTO>, IEnumerable<RubricViewModel>>(rubric);

            return View(news);
        }

        [HttpGet]
        public ActionResult CreateRubric()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateRubric(RubricViewModel rubric)
        {
            Mapper.Initialize(cfr => cfr.CreateMap<RubricDTO, RubricViewModel>()
                .ReverseMap());

            RubricDTO rubricDTO = Mapper.Map<RubricViewModel, RubricDTO>(rubric);

            newsService.SetRubric(rubricDTO);

            return RedirectToAction("RubricList");

        }

        [HttpGet]
        public ActionResult EditRubric(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            RubricDTO rubricDTO = newsService.GetRubric(id);

            if (rubricDTO != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<RubricDTO, RubricViewModel>());

                RubricViewModel news = Mapper.Map<RubricDTO, RubricViewModel>(rubricDTO);

                return View(news);
            }
            return HttpNotFound();

        }

        [HttpPost]
        public ActionResult EditRubric(RubricViewModel rubric)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<RubricViewModel, RubricDTO>()
                .ReverseMap());

            RubricDTO rubricDTO = Mapper.Map<RubricViewModel, RubricDTO>(rubric);
            newsService.UpdataRubric(rubricDTO);

            return RedirectToAction("RubricList");
        }

        [HttpGet]
        public ActionResult DeleteRubric(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            RubricDTO rubricDTO = newsService.GetRubric(id);

            if (rubricDTO == null)
            {
                return HttpNotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<RubricDTO, RubricViewModel>());

            RubricViewModel rubric = Mapper.Map<RubricDTO, RubricViewModel>(rubricDTO);
            return View(rubric);
        }

        [HttpPost, ActionName("DeleteRubric")]
        public ActionResult DeleteConfirmedRubric(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            newsService.DeleteRubric(id);

            return RedirectToAction("RubricList");
        }

        //работа с источниками даных
        public ActionResult SourceList()
        {
            IEnumerable<NewsSourceDTO> newsSourceDTO = newsService.GetAllNewsSources();
            Mapper.Initialize(cfg => cfg.CreateMap<NewsSourceDTO, SourceViewModel>());

            IEnumerable<SourceViewModel> sourceViewModel = Mapper.Map<IEnumerable<NewsSourceDTO>, IEnumerable<SourceViewModel>>(newsSourceDTO);

            return View(sourceViewModel);
        }

        [HttpGet]
        public ActionResult CreateSource()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateSource(SourceViewModel newsSource)
        {
            Mapper.Initialize(cfr => cfr.CreateMap<NewsSourceDTO, SourceViewModel>()
                .ReverseMap());

            NewsSourceDTO newsSourceDTO = Mapper.Map<SourceViewModel, NewsSourceDTO>(newsSource);

            newsService.SetNewsSource(newsSourceDTO);

            return RedirectToAction("SourceList");

        }

        [HttpGet]
        public ActionResult EditSource(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            NewsSourceDTO newsSourceDTO = newsService.GetNewsSource(id);

            if (newsSourceDTO != null)
            {
                Mapper.Initialize(cfg => cfg.CreateMap<NewsSourceDTO, SourceViewModel>());

                SourceViewModel sourceViewModel = Mapper.Map<NewsSourceDTO, SourceViewModel>(newsSourceDTO);

                return View(sourceViewModel);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult EditSource(SourceViewModel sourceViewModel)
        {
            Mapper.Initialize(cfg => cfg.CreateMap<SourceViewModel, NewsSourceDTO>()
                .ReverseMap());

            NewsSourceDTO newsSourceDTO = Mapper.Map<SourceViewModel, NewsSourceDTO>(sourceViewModel);
            newsService.UpdataSource(newsSourceDTO);

            return RedirectToAction("SourceList");
        }

        [HttpGet]
        public ActionResult DeleteSource(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            NewsSourceDTO newsSourceDTO = newsService.GetNewsSource(id);

            if (newsSourceDTO == null)
            {
                return HttpNotFound();
            }
            Mapper.Initialize(cfg => cfg.CreateMap<NewsSourceDTO, SourceViewModel>());

            SourceViewModel sourceViewModel = Mapper.Map<NewsSourceDTO, SourceViewModel>(newsSourceDTO);
            return View(sourceViewModel);
        }

        [HttpPost, ActionName("DeleteSource")]
        public ActionResult DeleteConfirmedSource(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            newsService.DeleteSource(id);

            return RedirectToAction("SourceList");
        }
    }
}