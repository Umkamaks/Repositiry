using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Entities;
using DAL.Interfaces;
using DAL.EF;
using System.Data.Entity;

namespace DAL.Repositories
{
    class UserRepository : IRepository<User>
    {
        private NewsContext newsContext;

        public UserRepository(NewsContext newsContext)
        {
            this.newsContext = newsContext;
        }
        public void Create(User item)
        {
            Role role = new Role {Name = "неопеределен"};
            item.Role = role;
            newsContext.Entry(item);
            newsContext.Users.Add(item);
            newsContext.SaveChanges();
        }

        public void Delete(int id)
        {
            User user = newsContext.Users.Find(id);
            if (user != null)
                newsContext.Users.Remove(user);
            newsContext.SaveChanges();
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return newsContext.Users.Where(predicate).ToList();
        }

        public User Get(int id)
        {
           return newsContext.Users.Find(id);
        }

        public IEnumerable<User> GetAll()
        {
            return newsContext.Users.Include(p=>p.Role).ToList();
        }

        public void Update(User item)
        {
            newsContext.Entry(item).State = EntityState.Modified;
            newsContext.SaveChanges();
        }
    }
}
