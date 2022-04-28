using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using CoreDemo.Models;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;

namespace CoreDemo.Controllers
{
	public class WriterController : Controller
	{
		WriterManager writerManager = new WriterManager(new EfWriterRepository());
		[Authorize]
		public IActionResult Index()
		{
			var userMail =User.Identity.Name;
			ViewBag.v3 = userMail;
			return View();
		}
		public IActionResult WriterProfile()
		{
			return View();
		}
		public IActionResult WriterMail()
		{
			return View();
		}
		[AllowAnonymous]
		public IActionResult Test()
		{
			return View();
		}
		[AllowAnonymous]
		public PartialViewResult WriterNavBarPartial()
		{
			return PartialView();
		}
		[AllowAnonymous]
		public PartialViewResult WriterFooterPartial()
		{
			return PartialView();
		}
		[HttpGet]
		public IActionResult WriterUpdateProfile()
		{
			BlogDbContext blogDbContext = new BlogDbContext();
			var userMail = User.Identity.Name;
			var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
			var writerValues = writerManager.GetById(writerId);
			return View(writerValues);
		}
		[HttpPost]
		public IActionResult WriterUpdateProfile(Writer writer)
		{
			WriterValidator writerValidator = new WriterValidator();
			ValidationResult results = writerValidator.Validate(writer);
			if(results.IsValid)
			{
				writerManager.Update(writer);
				return RedirectToAction("Index", "Dashboard");

			}
			else
			{
				foreach (var w in results.Errors)
				{
					ModelState.AddModelError(w.PropertyName, w.ErrorMessage);
				}
			}
			return View();
		}
		[AllowAnonymous]
		[HttpGet]
		public IActionResult WriterAdd()
		{
			return View();
		}
		[AllowAnonymous]
		[HttpPost]
		public IActionResult WriterAdd(AddProfileImage addProfileImage)
		{
			Writer writer = new Writer();
			if(addProfileImage.Image != null)
			{
				var extension = Path.GetExtension(addProfileImage.Image.FileName);
				var newImageName = Guid.NewGuid() + extension;
				var location= Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/writer/WriterImageFiles/", newImageName);	
				var stream= new FileStream(location, FileMode.Create);	
				addProfileImage.Image.CopyTo(stream);
				writer.Image = newImageName;
					//Globally Unique IDentifier” dır. ekleyeceğimiz resim dosyası adının aynı
					 //resim olsa bile arka tarafta farklı isimlerle kaydedilmesini sağlar.
					 //Yani resim dosyalarımız karışmasın diye bize benzersiz dosya adları eklememizi sağlar.
			}
			writer.Email = addProfileImage.Email;	
			writer.Password = addProfileImage.Password;	
			writer.FullName = addProfileImage.FullName;	
			writer.Status = true;	
			writer.About = addProfileImage.About;	
			writerManager.Add(writer);
			return RedirectToAction("Index", "Dashboard");
		}
	}
}
