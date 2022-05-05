using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminCommentController : Controller
    {
        CommentManager commentManger = new CommentManager(new EfCommentRepository());
        public IActionResult Index()
        {
            var values = commentManger.GetCommentWithBlog();
            return View(values);
        }
    }
}
