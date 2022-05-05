using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using CoreDemo.Models;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
	public class WriterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

        public WriterController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
		UserManager userManager = new UserManager(new EfUserRepository());
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
		public async Task<IActionResult> WriterUpdateProfile()
		{
			var values = await _userManager.FindByNameAsync(User.Identity.Name);
			UserUpdateViewModel userUpdateViewModel = new UserUpdateViewModel();
			userUpdateViewModel.eMail = values.Email;
			userUpdateViewModel.fullName = values.FullName;
			userUpdateViewModel.imageUrl = values.ImageUrl;
			userUpdateViewModel.userName = values.UserName;
			return View(userUpdateViewModel);
		}
		[HttpPost]
		public async Task<IActionResult> WriterUpdateProfile(UserUpdateViewModel userUpdateViewModel )
		{
			var values = await _userManager.FindByEmailAsync(User.Identity.Name);
			 values.Email = userUpdateViewModel.eMail ;
			 values.FullName = userUpdateViewModel.fullName;
			values.ImageUrl = userUpdateViewModel.imageUrl;
			values.PasswordHash = _userManager.PasswordHasher.HashPassword(values, userUpdateViewModel.password);
			var result = await _userManager.UpdateAsync(values);
			return RedirectToAction("Index", "Dashboard");
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
