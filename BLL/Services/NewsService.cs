using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            Mapper.Initialize(cfg => cfg.CreateMap<Rubric, RubricDTO>());
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
    }
}
