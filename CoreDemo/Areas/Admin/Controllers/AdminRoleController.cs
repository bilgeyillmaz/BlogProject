using CoreDemo.Areas.Admin.Models;
using Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreDemo.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles="Admin,Moderator")]
    public class AdminRoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;

        public AdminRoleController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index()
        {
            var values = _roleManager.Roles.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();  
        }
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel roleViewModel)
        {
            if(ModelState.IsValid)
            {
                AppRole role = new AppRole
                {
                    Name = roleViewModel.Name
                };
                var result= await _roleManager.CreateAsync(role);   
                if(result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var r in result.Errors)
                    {
                        ModelState.AddModelError("", r.Description);
                    }
                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult UpdateRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(r=>r.Id == id);
            RoleUpdateViewModel roleUpdateViewModel = new RoleUpdateViewModel
            {
                Id = values.Id,
                Name = values.Name
            };
            return View(roleUpdateViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRole(RoleUpdateViewModel roleUpdateViewModel)
        {
            var values= _roleManager.Roles.Where(r=>r.Id==roleUpdateViewModel.Id).FirstOrDefault();
            values.Name = roleUpdateViewModel.Name; 
            var result= await _roleManager.UpdateAsync(values); 
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View(roleUpdateViewModel);
        }
        public async Task<IActionResult> DeleteRole(int id)
        {
            var values = _roleManager.Roles.FirstOrDefault(r=>r.Id == id);
            var result= await _roleManager.DeleteAsync(values); 
            if(result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult UserRoleList()
        {
            var values = _userManager.Users.ToList();   
            return View(values);  
        }
        [HttpGet]
        public async Task<IActionResult> AssignRole(int id)
        {
            var user= _userManager.Users.FirstOrDefault(r=>r.Id==id);
            var roles = _roleManager.Roles.ToList();

            TempData["UserId"]= user.Id;
            var userRoles = await _userManager.GetRolesAsync(user);

            List<RoleAssignViewModel> roleAssignViewModels = new List<RoleAssignViewModel>();   
            foreach (var r in roles)
            {
                RoleAssignViewModel roleAssignViewModel = new RoleAssignViewModel();
                roleAssignViewModel.RoleId = r.Id;
                roleAssignViewModel.Name = r.Name;
                roleAssignViewModel.Exists = userRoles.Contains(r.Name);
                roleAssignViewModels.Add(roleAssignViewModel);
            }
            return View(roleAssignViewModels); 
        }
        [HttpPost]
        public async Task<IActionResult> AssignRole(List<RoleAssignViewModel> roleAssignViewModels)
        {
           var userId = (int)TempData["UserId"];
            var user = _userManager.Users.FirstOrDefault(x=>x.Id==userId);
            foreach (var r in roleAssignViewModels )
            {
                if(r.Exists)
                {
                    await _userManager.AddToRoleAsync(user,r.Name); //seçilen değerler listeye eklenecek
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user,r.Name);// seçili olmayan değerler listeden silinmiş olacak
                }
            }
            return RedirectToAction("UserRoleList");
        }
    }
}
