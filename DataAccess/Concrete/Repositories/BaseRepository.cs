using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.Repositories
{
    public class BaseRepository<T> : IBaseDal<T> where T : class
    {
        public void Delete(T t)
        {
           using var context= new BlogDbContext();    
            context.Remove(t);  
            context.SaveChanges();
        }

        public List<T> GetAll()
        {
            using var context = new BlogDbContext();
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            using var context = new BlogDbContext();
            return context.Set<T>().Find(id);
        }

        public void Insert(T t)
        {
            using var context = new BlogDbContext();
            context.Add(t);
            context.SaveChanges();
        }

        public List<T> GetListAll(Expression<Func<T, bool>> filter)
        {
            using var context = new BlogDbContext();
            return context.Set<T>().Where(filter).ToList();
        }

        public void Update(T t)
        {
            using var context = new BlogDbContext();
            context.Update(t);
            context.SaveChanges();
        }
    }
}
