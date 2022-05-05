using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace CoreDemo.ViewComponents.Writer
{
	public class WriterMessageNotification:ViewComponent
	{
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        BlogDbContext blogDbContext = new BlogDbContext();
        public IViewComponentResult Invoke()
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            var values = message2Manager.GetInboxListByWriter(writerId);
            return View(values);
        }
    }
}
