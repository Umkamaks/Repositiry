using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private NewsContext newsContext;
        private NewsRepository newsRepository;
        private NewsSourceRepository sourceRepository;
        private RubricRepository rubricRepository;

        public EFUnitOfWork(string connectionString)
        {
            newsContext = new NewsContext(connectionString);
        }


        public IRepository<News> News
        {
            get
            {
                if (newsRepository == null)
                    newsRepository = new NewsRepository(newsContext);
                return newsRepository;
            }
        }

        public IRepository<NewsSource> NewsSource
        {
            get
            {
                if (sourceRepository == null)
                    sourceRepository = new NewsSourceRepository(newsContext);
                return sourceRepository;
            }
        }

        public IRepository<Rubric> Rubric{
            get
            {
                if (rubricRepository == null)
                    rubricRepository = new RubricRepository(newsContext);
                return rubricRepository;
            }

        }

        public void Save()
        {
            newsContext.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    newsContext.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
