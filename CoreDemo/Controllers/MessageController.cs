using Business.Concrete;
using DataAccess.Concrete.Context;
using DataAccess.Concrete.EntityFramework;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class MessageController : Controller
    {   
        BlogDbContext blogDbContext = new BlogDbContext();
        Message2Manager message2Manager = new Message2Manager(new EfMessage2Repository());
        public IActionResult Inbox()
        {
            
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            var values = message2Manager.GetInboxListByWriter(writerId);
            return View(values);
        }
        public IActionResult MessageDetails(int id)
        {
                    var value = message2Manager.GetById(id);
            return View(value);
        }
        [HttpGet]
        public async Task<IActionResult> SendMessage()
        {
            List<SelectListItem> recieverUsers = (from u in await GetUsersAsync()
                                                  select new SelectListItem
                                                  {
                                                      Text = u.Email.ToString(),
                                                      Value = u.Id.ToString()
                                                  }).ToList();
            ViewBag.RecieverUser = recieverUsers;
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message2 message2)
        { //user Id kısayol
            //var enteredUsers = await _userManager.FindByIdAsync(User.Identity.Name); 
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            message2.SenderId = writerId;
            //message2.ReceiverId = receiverUsers;
            message2.Status = true;
            message2.Date = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            message2Manager.Add(message2);
            return RedirectToAction("Inbox");
        }
        public async Task<List<AppUser>> GetUsersAsync()
        {
            using (var context = new BlogDbContext())
            {
                return await context.Users.ToListAsync();
            }
        }

        public IActionResult SendBox()
        {
            var userName = User.Identity.Name;
            var userMail = blogDbContext.Users.Where(u => u.UserName == userName).Select(u => u.Email).FirstOrDefault();
            var writerId = blogDbContext.Writers.Where(w => w.Email == userMail).Select(w => w.Id).FirstOrDefault();
            var values = message2Manager.GetSendBoxListByWriter(writerId);
            return View(values);
        }
    }
}
