using dotnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Hosting.Server;
using dotnet_mvc.Services;

namespace dotnet_mvc.Controllers
{
    public class AuthController : Controller
    {
        private AuthService _authService;

        public AuthController(AuthService authService) {
            this._authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    var accessToken = this._authService.Login(loginModel);
                    Response.Cookies.Append("accessToken", accessToken);

                    return RedirectToAction("Index", "Home");

                }
                catch(SystemException e)
                {
                    
                    ModelState.AddModelError("LoginError", e.Message);
                }                     
            }
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupModel signupModel)
        {
            if(ModelState.IsValid)
            {
                try {
                    this._authService.Signup(signupModel);
                    return RedirectToAction("Login", "Auth");
                }
                catch(SystemException e)
                {
                    ModelState.AddModelError("SignupError", e.Message);
                }

            }
            return View();
        }

    }
}
