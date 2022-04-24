using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;

namespace CoreDemo.Controllers
{
	public class ContactController : Controller
	{
		ContactManager contactManager= new ContactManager(new EfContactRepository());
		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}
		[HttpPost]
		public IActionResult Index(Contact contact)
		{
			contact.CreateDate= DateTime.Parse(DateTime.Now.ToShortDateString());
			contact.Status = true;
			contactManager.Add(contact);
			return RedirectToAction("Index","Blog");	
		}
	}
}
