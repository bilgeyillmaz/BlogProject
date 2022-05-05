using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryChart()
        {
            List<CategoryModel> list = new List<CategoryModel>();
            list.Add(new CategoryModel
            { 
                name = "Teknoloji",
                count = 10
               
            } );
            list.Add(new CategoryModel
            {
                name = "Yazılım",
                count = 14
            });
            list.Add(new CategoryModel
            {
                name = "Spor",
                count = 5
            });
            list.Add(new CategoryModel
            {
                name = "Sinema",
                count = 2
                
            });
            return Json(new { jsonlist = list });
        }
    }
}
