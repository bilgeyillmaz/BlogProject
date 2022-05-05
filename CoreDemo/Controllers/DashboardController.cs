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
		[Authorize]
		public IActionResult Index()
		{
			BlogDbContext blogDbContext = new BlogDbContext();
			var userName = User.Identity.Name;
			var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
			var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();

			ViewBag.v1 = blogDbContext.Blogs.Count().ToString();
			ViewBag.v2 = blogDbContext.Blogs.Where(w=>w.WriterId ==writerId).Count();	
			ViewBag.v3=blogDbContext.Categories.Count().ToString();	
			return View();
		}
	}
}
