using DataAccess.Concrete.Context;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticFour:ViewComponent
    {
        BlogDbContext blogDbContext = new BlogDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = blogDbContext.Admins.Where(a => a.Id == 1).Select(a => a.Name).FirstOrDefault();
            ViewBag.v2 = blogDbContext.Admins.Where(a => a.Id == 1).Select(a => a.ImageURL).FirstOrDefault();
            ViewBag.v3 = blogDbContext.Admins.Where(a => a.Id == 1).Select(a => a.ShortDescription).FirstOrDefault();
            return View();
        }
    }
}
