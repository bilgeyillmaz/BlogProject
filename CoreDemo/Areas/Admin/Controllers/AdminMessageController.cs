using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminMessageController : Controller
    {
        BlogDbContext blogDbContext = new BlogDbContext();
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        private readonly UserManager<AppUser> _userManager;
        public AdminMessageController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Inbox()
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            var values = message2Manager.GetInboxListByWriter(writerId);
            return View(values);
        }
        public IActionResult SendBox()
        {
            var username = User.Identity.Name;
            var usermail = blogDbContext.Users.Where(u => u.UserName == username).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w=>w.Email == usermail).Select(w => w.Id).FirstOrDefault();
            var values = message2Manager.GetSendBoxListByWriter(writerId);
            return View(values);
        }
        [HttpGet]
        public IActionResult ComposeMessage()
        {
            return View(Tuple.Create<Message2, AppUser>(new Message2(), new AppUser()));
        }
        [HttpPost]
        public async Task<IActionResult> ComposeMessage([Bind(Prefix = "Item1")] Message2 message, [Bind(Prefix = "Item2")] AppUser writer)
        {
            var sender = await _userManager.FindByNameAsync(User.Identity.Name);
            var receiver = await _userManager.FindByEmailAsync(writer.Email);
            message.SenderId = sender.Id;
            message.ReceiverId = receiver.Id;
            message.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message.Status = true;
            message2Manager.Add(message);
            return RedirectToAction("SendBox");
        }
    }
}