using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.ViewComponents.Writer
{
	public class WriterAnnouncementNotification: ViewComponent
    {
        NotificationManager notificationManager= new NotificationManager(new EfNotificationRespository());
        public IViewComponentResult Invoke()
        {
            var values = notificationManager.GetAll();
            return View(values);
        }
    }
}
