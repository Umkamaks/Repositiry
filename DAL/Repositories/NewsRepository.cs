using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class NewsRepository : IRepository<News>
    {
        private NewsContext newsContext;

        public NewsRepository(NewsContext newsContext)
        {
            this.newsContext = newsContext;
        }
        public void Create(News item)
        {
            newsContext.News.Add(item);
            newsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            News news = newsContext.News.Find(id);
            
            if (news != null)
                newsContext.News.Remove(news);
            newsContext.SaveChanges();

        }

        public IEnumerable<News> Find(Func<News, bool> predicate)
        {
            return newsContext.News.Include(rub => rub.Rubric).Include(nsrc => nsrc.NewsSource).Where(predicate).ToList();
        }

        public News Get(int id)
        {
            return newsContext.News.Find(id);
        }

        public IEnumerable<News> GetAll()
        {
            return newsContext.News.Include(rub => rub.Rubric).Include(nsrc => nsrc.NewsSource).ToList();
        }

        public void Update(News item)
        {
            newsContext.Entry(item).State = EntityState.Modified;
            newsContext.SaveChanges();
        }
    }
}
