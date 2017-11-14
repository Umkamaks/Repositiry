using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class NewsSourceRepository : IRepository<NewsSource>
    {
        private NewsContext newsContext;

        public NewsSourceRepository(NewsContext newsContext)
        {
            this.newsContext = newsContext;
        }
        public void Create(NewsSource item)
        {
            newsContext.NewsSources.Add(item);
            newsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            NewsSource newsSource = newsContext.NewsSources.Find(id);
            newsContext.Entry(newsSource).Collection(p => p.News).Load();
            newsContext.NewsSources.Remove(newsSource);
            newsContext.SaveChanges(); 
        }

        public IEnumerable<NewsSource> Find(Func<NewsSource, bool> predicate)
        {
            return newsContext.NewsSources.Include(news=>news.News).Where(predicate).ToList();
        }

        public NewsSource Get(int id)
        {
            return newsContext.NewsSources.Find(id);
        }

        public IEnumerable<NewsSource> GetAll()
        {
            return newsContext.NewsSources.Include(news => news.News);
        }

        public void Update(NewsSource item)
        {
            newsContext.Entry(item).State = EntityState.Modified;
            newsContext.SaveChanges();
        }
    }
}
