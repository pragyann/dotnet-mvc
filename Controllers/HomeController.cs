using Microsoft.AspNetCore.Mvc;

namespace dotnet_mvc.Controllers
{
    public class HomeController : Controller
    {
        public HomeController() { }

        [Route("/")]
        public IActionResult Index()
        {
            var accessToken = Request.Cookies["accessToken"];

            ViewBag.accessToken = accessToken;

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
