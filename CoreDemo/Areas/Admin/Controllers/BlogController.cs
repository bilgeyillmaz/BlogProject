using ClosedXML.Excel;
using CoreDemo.Areas.Admin.Models;
using DataAccess.Concrete.Context;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        public IActionResult ExportStaticExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";
                int blogRowCount = 2;
                foreach (var b in GetBlogList())
                {
                    worksheet.Cell(blogRowCount, 1).Value = b.ID;
                    worksheet.Cell(blogRowCount, 2).Value = b.BlogName;
                    blogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFileExample.xlsx");
                }
            }
        }
        public List<BlogModel> GetBlogList()
        {
            List<BlogModel> blogModels = new List<BlogModel>
            {
                new BlogModel{ID=1, BlogName="C# Programlamaya Giriş"},
                new BlogModel{ID=2, BlogName="Tesla firmasının araçları"},
                new BlogModel{ID=3, BlogName="2020 Olimpiyatları"}
            };
            return blogModels;
        }
        public IActionResult BlogListExcel()
        {
            return View();
        }
        public IActionResult ExportDynamicExcelBlogList()
        {
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Blog Listesi");
                worksheet.Cell(1, 1).Value = "Blog ID";
                worksheet.Cell(1, 2).Value = "Blog Adı";
                int blogRowCount = 2;
                foreach (var b in BlogTitleList())
                {
                    worksheet.Cell(blogRowCount, 1).Value = b.ID;
                    worksheet.Cell(blogRowCount, 2).Value = b.BlogName;
                    blogRowCount++;
                }
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ExcelFileExample.xlsx");
                }
            }
        }
        public List<BlogModelSecond> BlogTitleList()
        {
            List<BlogModelSecond> blogModelSeconds = new List<BlogModelSecond>();
            using(var blogDbContext = new BlogDbContext())
            {
                blogModelSeconds = blogDbContext.Blogs.Select(b => new BlogModelSecond
                {
                    ID = b.Id,
                    BlogName=b.Title,
                }).ToList();
            }
            return blogModelSeconds;
        }
        public IActionResult BlogTitleListExcel()
        {
            return View();
        }
    }
}
