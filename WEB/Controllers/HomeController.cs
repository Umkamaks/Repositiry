using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using WEB.Models;
using System.Data.Entity;
using PagedList;

namespace WEB.Controllers
{
    public class HomeController : Controller
    {
        INewsService newsService;

        public HomeController(INewsService newsService)
        {
            this.newsService = newsService;
        }

        public ActionResult Index(string rubric, int? page)
        {
            int pageSize = 3;
            int pageNumber = page ?? 1;

            IEnumerable<NewsDTO> newsDtos = newsService.GetAllNews();

            Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>()
                .ForMember(d => d.RubricName, opt => opt.MapFrom(src => src.Rubric.NameRubric))
                .ForMember(d => d.SourceName, opt => opt.MapFrom(src => src.NewsSource.NameSourceNews)));


            var news = Mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos)
                .Where(p => rubric == null || p.RubricName == rubric)
                .OrderByDescending(i => i.Id);

            IEnumerable<RubricDTO> rubricDTO = newsService.GetAllRubrics();

            Mapper.Initialize(cfg => cfg.CreateMap<RubricDTO, RubricViewModel>()
                .ForMember(p => p.News, opt => opt.MapFrom(src => src.News)));

            var rubrics = Mapper.Map<IEnumerable<RubricDTO>, IEnumerable<RubricViewModel>>(rubricDTO);

            NewsRubricViewModel newsRubricViewModel = new NewsRubricViewModel
            {
                NewsViewModel = news,
                RubricViewModel = rubrics
            };
            ViewBag.News = news.ToPagedList(pageNumber, pageSize);
            ViewBag.Rubrics = rubrics;
            return View(newsRubricViewModel);
        }


        public ActionResult News(int? id, string rubric)
        {
            IEnumerable<NewsDTO> newsDtos = newsService.GetAllNews();

            Mapper.Initialize(cfg => cfg.CreateMap<NewsDTO, NewsViewModel>()
                .ForMember(d => d.RubricName, opt => opt.MapFrom(src => src.Rubric.NameRubric))
                .ForMember(d => d.SourceName, opt => opt.MapFrom(src => src.NewsSource.NameSourceNews)));


            var news = Mapper.Map<IEnumerable<NewsDTO>, List<NewsViewModel>>(newsDtos)
                .Where(p => rubric == null || p.RubricName == rubric)
                .OrderByDescending(i => i.Id);

            IEnumerable<RubricDTO> rubricDTO = newsService.GetAllRubrics();

            Mapper.Initialize(cfg => cfg.CreateMap<RubricDTO, RubricViewModel>()
                .ForMember(p => p.News, opt => opt.MapFrom(src => src.News)));

            var rubrics = Mapper.Map<IEnumerable<RubricDTO>, IEnumerable<RubricViewModel>>(rubricDTO);

            NewsRubricViewModel newsRubricViewModel = new NewsRubricViewModel
            {
                NewsDTO = newsService.GetNews(id),
                NewsViewModel = news,
                RubricViewModel = rubrics
            };
     
            return View(newsRubricViewModel);
        }

    }
}