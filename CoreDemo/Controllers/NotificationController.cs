using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Controllers
{
	public class NotificationController : Controller
	{
		NotificationManager notificationManager = new NotificationManager(new EfNotificationRespository());
		public IActionResult Index()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult AllNotification()
        {
			var values = notificationManager.GetAll();
			return View(values);
        }
	}
}
