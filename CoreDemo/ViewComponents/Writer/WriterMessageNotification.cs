using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
	public class WriterMessageNotification:ViewComponent
	{
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        public IViewComponentResult Invoke()
        {
            int id = 2;
            var values = message2Manager.GetInboxListByWriter(id);
            return View(values);
        }
    }
}
