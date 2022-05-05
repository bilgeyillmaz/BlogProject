using DataAccess.Concrete.Context;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticThree:ViewComponent
    {
        BlogDbContext blogDbContext = new BlogDbContext();
        public IViewComponentResult Invoke()
        {
            //ViewBag.v1 = blogDbContext.Blogs.OrderByDescending(b => b.Id).Select(b => b.Title).Take(1).FirstOrDefault();
            //ViewBag.v3 = blogDbContext.Comments.Count();
            return View();
        }
    }
}
