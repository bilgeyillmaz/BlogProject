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
    public class EfCommentRepository : BaseRepository<Comment>, ICommentDal
    {
        public List<Comment> GetListWithBlog()
        {
            using (var blogDbContext = new BlogDbContext())
            {
                return blogDbContext.Comments.Include(c => c.Blog).ToList();
            }
        }
    }
}
