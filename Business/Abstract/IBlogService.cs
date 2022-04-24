using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IBlogService : IBaseService<Blog>
    {
        List<Blog> GetAllBlogswithCategory();
        List<Blog> GetBlogListWithWriter(int id);
    }
        
}
