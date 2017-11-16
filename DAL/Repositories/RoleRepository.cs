using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.EF;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    class RoleRepository : IRepository<Role>
    {
        private NewsContext newsContext;

        public RoleRepository(NewsContext newsContext)
        {
            this.newsContext = newsContext;
        }
        public void Create(Role item)
        {
            newsContext.Roles.Add(item);
            newsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            Role role = newsContext.Roles.Find(id);
            if (role != null)
                newsContext.Roles.Remove(role);
            newsContext.SaveChanges();
        }

        public IEnumerable<Role> Find(Func<Role, bool> predicate)
        {
            return newsContext.Roles.Where(predicate).ToList();
        }

        public Role Get(int id)
        {
            return newsContext.Roles.Find(id);
        }

        public IEnumerable<Role> GetAll()
        {
            return newsContext.Roles.ToList();
        }

        public void Update(Role item)
        {
            newsContext.Entry(item).State = EntityState.Modified;
            newsContext.SaveChanges();
        }
    }
}
