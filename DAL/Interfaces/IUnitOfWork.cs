using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
   public interface IUnitOfWork:IDisposable
    {
        IRepository<News> News { get; }
        IRepository<NewsSource> NewsSource { get; }
        IRepository<Rubric> Rubric { get; }
        void Save();
    }
}
