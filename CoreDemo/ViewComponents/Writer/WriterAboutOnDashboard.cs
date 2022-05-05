using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.Writer
{
	public class WriterAboutOnDashboard:ViewComponent
	{
       WriterManager writerManager = new WriterManager(new EfWriterRepository());
		BlogDbContext blogDbContext = new BlogDbContext();
		public IViewComponentResult Invoke()
		{	
			var userName = User.Identity.Name;
			ViewBag.v = userName;
			var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
			var writerId= blogDbContext.Writers.Where(w=>w.Email == userMail).Select(w=>w.Id).FirstOrDefault();	
			var values = writerManager.GetWriterById(writerId);
			return View(values);
		}
	}
}
