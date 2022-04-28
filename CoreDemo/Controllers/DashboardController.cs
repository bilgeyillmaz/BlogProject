using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.Controllers
{
	public class DashboardController : Controller
	{
		BlogManager blogManager = new BlogManager(new EfBlogRepository());

		[AllowAnonymous]
		public IActionResult Index()
		{
			BlogDbContext blogDbContext = new BlogDbContext();
			ViewBag.v1 = blogDbContext.Blogs.Count().ToString();
			ViewBag.v2 = blogDbContext.Blogs.Where(w=>w.WriterId ==1).Count();	
			ViewBag.v3=blogDbContext.Categories.Count().ToString();	
			return View();
		}
	}
}
