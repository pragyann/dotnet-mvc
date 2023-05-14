using Microsoft.AspNetCore.Mvc;

namespace dotnet_mvc.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult AddStudent()
        {
            return View();
        }
        
        public IActionResult GetStudent() {
            return View();
        }
    }
}
