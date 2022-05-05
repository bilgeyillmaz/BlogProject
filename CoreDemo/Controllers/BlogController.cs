using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class BlogController : Controller
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        CategoryManager categoryManager = new CategoryManager(new EfCategoryRepository());
        BlogDbContext blogDbContext = new BlogDbContext();

      //  [AllowAnonymous]
        public IActionResult Index()
        {
            var values = blogManager.GetAllBlogswithCategory();
            return View(values);
        }
       // [AllowAnonymous]
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.Id = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
        }
        public IActionResult BlogListWithWriter()
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            var values = blogManager.GetListWithCategoryByWriterBM(writerId);
            return View(values);
        }
        [HttpGet]
        public IActionResult BlogAdd()
        {
            List<SelectListItem> categoryValues = (from c in categoryManager.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.Id.ToString()
                                                   }).ToList();
            ViewBag.cV = categoryValues;

            return View();
        }
        [HttpPost]
        public IActionResult BlogAdd(Blog blog)
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult result = blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.Status = true;
                blog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterId = writerId;
                blogManager.Add(blog);
                return RedirectToAction("BlogListWithWriter", "Blog");
            }
            else
            {
                foreach (var b in result.Errors)
                {
                    ModelState.AddModelError(b.PropertyName, b.ErrorMessage);
                }
                return View();
            }
        }
        public IActionResult DeleteBlog(int id)
		{
            var blogValue= blogManager.GetById(id); 
            blogManager.Delete(blogValue);  
            return RedirectToAction("BlogListWithWriter");
		}
        [HttpGet]
        public IActionResult UpdateBlog(int id)
		{
            List<SelectListItem> categoryValues = (from c in categoryManager.GetAll()
                                                   select new SelectListItem
                                                   {
                                                       Text = c.Name,
                                                       Value = c.Id.ToString()
                                                   }).ToList();
            ViewBag.cV = categoryValues;
            var blogValue = blogManager.GetById(id);
            return View(blogValue);
		}
        [HttpPost]
        public IActionResult UpdateBlog(Blog blog)
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            blog.WriterId = writerId;
            blog.CreateDate= DateTime.Parse( DateTime.Now.ToShortDateString());
            blog.Status = true;
            blogManager.Update(blog);   
            return RedirectToAction("BlogListWithWriter");
        }

    }
}
