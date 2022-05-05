using CoreDemo.Models;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    [AllowAnonymous]
    public class RegisterUserController : Controller
    {
        private readonly UserManager<AppUser> _userManager; //mimarinin dışına çıkıldı, service ya da manager işlemi gerçekleştirilmedi. Identity
        //kütüphanesinin kendi metotları kullanılacak.

        public RegisterUserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserSignUpViewModel userSignUpViewModel)
        {
            if(ModelState.IsValid)
            {
                AppUser user= new AppUser()
                {
                    Email = userSignUpViewModel.Email,
                    UserName = userSignUpViewModel.UserName,
                    FullName = userSignUpViewModel.FullName
                };
                var result = await _userManager.CreateAsync(user, userSignUpViewModel.Password);
                if(result.Succeeded)
                {
                    return RedirectToAction("Index", "Login");
                }
                else
                {
                    foreach(var u in result.Errors)
                    {
                        ModelState.AddModelError("", u.Description);
                    }
                }
            }
            return View(userSignUpViewModel);
        }
    }
}
