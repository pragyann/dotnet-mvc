using Microsoft.AspNetCore.Mvc;

namespace dotnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        [Route("/")]
        public IActionResult Index(string email,string password)
        {   
            
            if(email!=null && password != null)
            {
                ViewBag.Email = email;
                ViewBag.Password = password;
            }   
            return View();
        }

        public IActionResult Gallery()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        public IActionResult ErrorPage()
        {
            return View();
        }
    }
}
