using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Blog
{
	public class BlogListDashboard:ViewComponent
	{
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        public IViewComponentResult Invoke()
        {
            var values = blogManager.GetAllBlogswithCategory();
            return View(values);
        }
    }
}
