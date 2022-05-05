using BlogAPI.DataAccess;
using BlogAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BlogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DefaultController : ControllerBase
    {
        [HttpGet]
        public IActionResult EmployeeList()
        {
            using var blogProjectDbContext = new BlogProjectDbContext();
            var values = blogProjectDbContext.Employees.ToList();
            return Ok(values);
        }
        [HttpPost]
        public IActionResult EmployeeAdd(Employee employee)
        {
            using var blogProjectDbContext = new BlogProjectDbContext();
            blogProjectDbContext.Add(employee);
            blogProjectDbContext.SaveChanges();
            return Ok();
        }
        [HttpGet("{id}")]
        public IActionResult EmployeeGet(int id)
        {
            using var blogProjectDbContext = new BlogProjectDbContext();
            var employee = blogProjectDbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(employee);
            }
        }
        [HttpDelete("{id}")]
        public IActionResult EmployeeDelete(int id)
        {
            using var blogProjectDbContext = new BlogProjectDbContext();
            var employee = blogProjectDbContext.Employees.Find(id);
            if (employee == null)
            {
                return NotFound();
            }
            else
            {
                blogProjectDbContext.Remove(employee);
                blogProjectDbContext.SaveChanges();
                return Ok();
            }
        }
        [HttpPut]
        public IActionResult EmployeeUpdate(Employee employee)
        {
            using var blogProjectDbContext = new BlogProjectDbContext();
            var emp = blogProjectDbContext.Find<Employee>(employee.Id);
            if (emp == null)
            {
                return NotFound();
            }
            else
            {
                emp.Name = employee.Name;
                blogProjectDbContext.Update(emp);
                blogProjectDbContext.SaveChanges();
                return Ok();
            }
        }
    }
}
