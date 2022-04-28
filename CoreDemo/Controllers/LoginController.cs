using DataAccess.Concrete.Context;
using Entity.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class LoginController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Index(Writer writer)
        {
            BlogDbContext blogDbContext = new BlogDbContext();
            var dataValue = blogDbContext.Writers.FirstOrDefault(w => w.Email == writer.Email && w.Password == writer.Password);
            if (dataValue != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, writer.Email)
                };
                var userIdentity = new ClaimsIdentity(claims, "a");  
                ClaimsPrincipal principal= new ClaimsPrincipal(userIdentity);
                await HttpContext.SignInAsync(principal);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        //         BlogDbContext blogDbContext = new BlogDbContext();
        //         var dataValue = blogDbContext.Writers.FirstOrDefault(w => w.Email == writer.Email && w.Password == writer.Password);
        //         if(dataValue != null)
        //{
        //             HttpContext.Session.SetString("username", writer.Email);
        //             return RedirectToAction("Index", "Writer");   
        //}
        //         else
        //{
        //              return View();
        //}


    }
}
