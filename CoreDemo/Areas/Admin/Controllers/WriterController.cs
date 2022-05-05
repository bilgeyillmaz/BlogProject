using CoreDemo.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WriterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult WriterList()
        {
            var jsonWriters = JsonConvert.SerializeObject(writers);
            return Json(jsonWriters);
        }
        public IActionResult GetWriterById(int writerid)
        {
            var findWriter = writers.FirstOrDefault(w => w.Id == writerid);
            var jsonWriters = JsonConvert.SerializeObject(findWriter);
            return Json(jsonWriters);
        }
        [HttpPost]
        public IActionResult AddWriter(WriterModel writer)
        {
            writers.Add(writer);
            var jsonWriters = JsonConvert.SerializeObject(writer);
            return Json(jsonWriters);

        }
        public IActionResult DeleteWriter(int id)
        {
            var writer = writers.FirstOrDefault(w => w.Id == id);
            writers.Remove(writer);
            return Json(writer);
        }
        public IActionResult UpdateWriter(WriterModel writer)
        {
            var updatedWriter = writers.FirstOrDefault(w => w.Id == writer.Id);
            updatedWriter.Name = writer.Name;
            var jsonWriter = JsonConvert.SerializeObject(writer);
            return Json(jsonWriter);
        }
        public static List<WriterModel> writers = new List<WriterModel>
        {
            new WriterModel
            {
                Id=1,
                Name="Bilgi"
            },
            new WriterModel
            {
                Id=2,
                Name="Gözde"
            },
            new WriterModel
            {
                Id=3,
                Name="Ece"
            },
                new WriterModel
            {
                Id=4,
                Name="Efe"
            }
        };
    }
}
