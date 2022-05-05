using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreDemo.Controllers
{
    public class EmployeeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var httpClient = new HttpClient();
            var responceMessage = await httpClient.GetAsync("https://localhost:44305/api/Default");
            var jsonString = await responceMessage.Content.ReadAsStringAsync();
            var values = JsonConvert.DeserializeObject<List<EmployeeApiModel>>(jsonString);
            return View(values);
        }

        [HttpGet]
        public IActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee(EmployeeApiModel employeeApiModel)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(employeeApiModel);
            StringContent content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responceMessage = await httpClient.PostAsync("https://localhost:44305/api/Default", content);
            if (responceMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(employeeApiModel);
        }
        [HttpGet]
        public async Task<IActionResult> UpdateEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responceMessage = await httpClient.GetAsync("https://localhost:44305/api/Default" + id);
            if (responceMessage.IsSuccessStatusCode)
            {
                var jsonEmployee = await responceMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<EmployeeApiModel>(jsonEmployee);
                return View(values);
            }
            //return RedirectToAction("Index"); ????
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(EmployeeApiModel employeeApiModel)
        {
            var httpClient = new HttpClient();
            var jsonEmployee = JsonConvert.SerializeObject(employeeApiModel);
            var content = new StringContent(jsonEmployee, Encoding.UTF8, "application/json");
            var responceMessage = await httpClient.PutAsync("https://localhost:44305/api/Default", content);
            if (responceMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View(employeeApiModel);
        }
        public async Task<IActionResult>DeleteEmployee(int id)
        {
            var httpClient = new HttpClient();
            var responceMessage = await httpClient.DeleteAsync("https://localhost:44305/api/Default" + id);
            if(responceMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
    public class EmployeeApiModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
