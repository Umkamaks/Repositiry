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
    public class RubricRepository : IRepository<Rubric>
    {
        private NewsContext newsContext;

        public RubricRepository(NewsContext newsContext)
        {
            this.newsContext = newsContext;
        }
        public void Create(Rubric item)
        {
            newsContext.Rubrics.Add(item);
            newsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Rubric rubric = newsContext.Rubrics.Find(id);
       
            newsContext.Entry(rubric).Collection(p => p.News).Load();
            newsContext.Rubrics.Remove(rubric);
            newsContext.SaveChanges();
        }

        public IEnumerable<Rubric> Find(Func<Rubric, bool> predicate)
        {
            return newsContext.Rubrics.Include(news => news.News).Where(predicate).ToList();
        }

        public Rubric Get(int id)
        {
            return newsContext.Rubrics.Find(id);
        }

        public IEnumerable<Rubric> GetAll()
        {
            return newsContext.Rubrics.Include(news => news.News);
        }

        public void Update(Rubric item)
        {
            newsContext.Entry(item).State = EntityState.Modified;
            newsContext.SaveChanges();
        }
    }
}
