using Business.Concrete;
using Business.ValidationRules.FluentValidation;
using DataAccess.Abstract;
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

        public IActionResult Index()
        {
            var values = blogManager.GetAllBlogswithCategory();
            return View(values);
        }
        public IActionResult BlogReadAll(int id)
        {
            ViewBag.Id = id;
            var values = blogManager.GetBlogById(id);
            return View(values);
        }
        public IActionResult BlogListWithWriter()
        {
            var values = blogManager.GetListWithCategoryByWriterBM(1);
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
            BlogValidator blogValidator = new BlogValidator();
            ValidationResult result = blogValidator.Validate(blog);
            if (result.IsValid)
            {
                blog.Status = true;
                blog.CreateDate = DateTime.Parse(DateTime.Now.ToShortDateString());
                blog.WriterId = 1;
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
            return RedirectToAction("BlogListWithWriter");
        }

    }
}
