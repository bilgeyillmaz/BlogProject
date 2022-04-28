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
    public class EfMessage2Repository : BaseRepository<Message2>, IMessage2Dal
    {
        public List<Message2> GetListWithMessageByWriter(int id)
        {
            using (var blogDbContext = new BlogDbContext())
            {
                return blogDbContext.Message2s.Include(m => m.SenderUser).Where(m => m.ReceiverId == id).ToList();
            }
        }
    }
}
