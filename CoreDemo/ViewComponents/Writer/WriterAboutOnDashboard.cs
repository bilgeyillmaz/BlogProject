using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
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
			var userMail = User.Identity.Name;
			var writerId= blogDbContext.Writers.Where(w=>w.Email == userMail).Select(w=>w.Id).FirstOrDefault();	
			var values = writerManager.GetWriterById(writerId);
			return View(values);
		}
	}
}
