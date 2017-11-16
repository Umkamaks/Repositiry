using System.Collections.Generic;
using AutoMapper;
using BLL.DTO;
using BLL.Infrastructure;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class NewsService : INewsService
    {
        IUnitOfWork Database { get; set; }

        public NewsService(IUnitOfWork dataBase)
        {
            Database = dataBase;
        }

        public void DeleteNews(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id новости", "");
            Database.News.Delete(id.Value);

        }

        public void DeleteRubric(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id рубрики", "");
            Database.Rubric.Delete(id.Value);
        }

        public void Dispose()
        {
            Database.Dispose();
        }

        public IEnumerable<NewsDTO> GetAllNews()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<News, NewsDTO>());
            return Mapper.Map<IEnumerable<News>, List<NewsDTO>>(Database.News.GetAll());
        }

        public IEnumerable<NewsSourceDTO> GetAllNewsSources()
        {
            Mapper.Initialize(cfg => cfg.CreateMap<NewsSource, NewsSourceDTO>());
            return Mapper.Map<IEnumerable<NewsSource>, List<NewsSourceDTO>>(Database.NewsSource.GetAll());
        }

        public IEnumerable<RubricDTO> GetAllRubrics()
        {

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<News, NewsDTO>();
                cfg.CreateMap<Rubric, RubricDTO>()
                .ForMember(p => p.News, opt => opt.MapFrom(src => src.News));
            });
            Mapper.Configuration.AssertConfigurationIsValid();
            return Mapper.Map<IEnumerable<Rubric>, List<RubricDTO>>(Database.Rubric.GetAll());
        }

        public NewsDTO GetNews(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id новости", "");
            var news = Database.News.Get(id.Value);

            if (news == null)
                throw new ValidationException("Не найдена новость", "");
            Mapper.Initialize(cfg => cfg.CreateMap<News, NewsDTO>());
            return Mapper.Map<News, NewsDTO>(news);
        }

        public NewsSourceDTO GetNewsSource(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id источника новости", "");
            var newsSource = Database.NewsSource.Get(id.Value);

            if (newsSource == null)
                throw new ValidationException("Источник новости не найден", "");
            Mapper.Initialize(cfg => cfg.CreateMap<NewsSource, NewsSourceDTO>());
            return Mapper.Map<NewsSource, NewsSourceDTO>(newsSource);
        }

        public RubricDTO GetRubric(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id рубрики", "");
            var rubric = Database.Rubric.Get(id.Value);

            if (rubric == null)
                throw new ValidationException("Рубрика не найдена", "");
            Mapper.Initialize(cfg => cfg.CreateMap<Rubric, RubricDTO>());
            return Mapper.Map<Rubric, RubricDTO>(rubric);
        }

        public void SetNews(NewsDTO newsDTO)
        {
            if (newsDTO == null)
                throw new ValidationException("Пустая новость", "");

            Mapper.Initialize(cfr => cfr.CreateMap<NewsDTO, News>());

            News news = Mapper.Map<NewsDTO, News>(newsDTO);
            Database.News.Create(news);
        }


        public void UpdataNews(NewsDTO newsDTO)
        {
            if (newsDTO == null)
                throw new ValidationException("Пустая новость", "");
            Mapper.Initialize(cfr => cfr.CreateMap<NewsDTO, News>());
            News news = Mapper.Map<NewsDTO, News>(newsDTO);
            Database.News.Update(news);
        }

        public void SetRubric(RubricDTO rubricDTO)
        {
            if (rubricDTO == null)
                throw new ValidationException("Пустая рубрика", "");
            Mapper.Initialize(cfr => cfr.CreateMap<RubricDTO, Rubric>());

            Rubric rubric = Mapper.Map<RubricDTO, Rubric>(rubricDTO);
            Database.Rubric.Create(rubric);
        }

        public void UpdataRubric(RubricDTO rubricDTO)
        {
            if (rubricDTO == null)
                throw new ValidationException("Пустая рубрика", "");
            Mapper.Initialize(cfr => cfr.CreateMap<RubricDTO, Rubric>());
            Rubric rubric = Mapper.Map<RubricDTO, Rubric>(rubricDTO);
            Database.Rubric.Update(rubric);
        }

        public void SetNewsSource(NewsSourceDTO newsSourceDTO)
        {
            if (newsSourceDTO == null)
                throw new ValidationException("Пустой источник новости", "");
            Mapper.Initialize(cfr => cfr.CreateMap<NewsSourceDTO, NewsSource>());

            NewsSource newsSource = Mapper.Map<NewsSourceDTO, NewsSource>(newsSourceDTO);
            Database.NewsSource.Create(newsSource);
        }

        public void UpdataSource(NewsSourceDTO newsSourceDTO)
        {
            if (newsSourceDTO == null)
                throw new ValidationException("Пустой источник новости", "");
            Mapper.Initialize(cfr => cfr.CreateMap<NewsSourceDTO, NewsSource>());
            NewsSource newsSource = Mapper.Map<NewsSourceDTO, NewsSource>(newsSourceDTO);
            Database.NewsSource.Update(newsSource);
        }

        public void DeleteSource(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлен Id источника новости", "");
            Database.NewsSource.Delete(id.Value);
        }
        //private static ISet<T> ToISet<T>(IEnumerable<T> list)
        //{
        //    ISet<T> set = null;

        //    if (list != null)
        //    {
        //        set = new HashSet<T>();

        //        foreach (T item in list)
        //        {
        //            set.Add(item);
        //        }
        //    }

        //    return set;
        //}
        //private static ISet<TDestination> ToISet<TSource, TDestination>(IEnumerable<TSource> source)
        //{
        //    ISet<TDestination> set = null;
        //    if (source != null)
        //    {
        //        set = new HashSet<TDestination>();

        //        foreach (TSource item in source)
        //        {
        //            set.Add(Mapper.Map<TSource, TDestination>(item));
        //        }
        //    }
        //    return set;
        //}
    }
}
