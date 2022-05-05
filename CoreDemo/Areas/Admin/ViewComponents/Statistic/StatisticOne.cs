using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Xml.Linq;

namespace CoreDemo.Areas.Admin.ViewComponents.Statistic
{
    public class StatisticOne:ViewComponent
    {
        BlogManager blogManager = new BlogManager(new EfBlogRepository());
        BlogDbContext blogDbContext = new BlogDbContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.v1 = blogManager.GetAll().Count();
            ViewBag.v2 = blogDbContext.Contacts.Count();
            ViewBag.v3 = blogDbContext.Comments.Count();

            string api = "1a60c11bf4e9fd8cdd0670f54c585f3f";
            string connection = " https://api.openweathermap.org/data/2.5/weather?q=istanbul&mode=xml&lang=tr&units=metric&appid="+api;
            XDocument document = XDocument.Load(connection);
            ViewBag.temperature = document.Descendants("temperature").ElementAt(0).Attribute("value").Value;
            return View();
        }
    }
}
