using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.Repositories;
using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBlogRepository : BaseRepository<Blog>, IBlogDal
    {
        public List<Blog> GetAllWithCategory()
        {
            using(var blogDbContext= new BlogDbContext())
            {
                return blogDbContext.Blogs.Include(c => c.Category).ToList();
            }
        }
    }
}
