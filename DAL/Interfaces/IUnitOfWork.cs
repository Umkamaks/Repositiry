using System;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<News> News { get; }
        IRepository<NewsSource> NewsSource { get; }
        IRepository<Rubric> Rubric { get; }
        IRepository<Role> Role { get; }
        IRepository<User> User { get; }
        void Save();
    }
}
