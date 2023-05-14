using dotnet_mvc.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.SqlServer.Server;
using Microsoft.AspNetCore.Hosting.Server;

namespace dotnet_mvc.Controllers
{
    public class AuthController : Controller
    {
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
                string connectionConfig = "Server = localhost; Database = dotnet_mvc; Integrated Security = True; ";
                SqlConnection conn = new(connectionConfig);
                string sql = "SELECT * FROM users WHERE email=@Email";
                SqlCommand cmd = new(sql, conn);
        
                cmd.Parameters.AddWithValue("@Email", loginModel.Email);  

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();

                if (!reader.HasRows)
                {
                    ModelState.AddModelError("LoginError", "Invalid Credentials. Please try again.");
                    return View();
                }

                string hashedPassword = reader.GetFieldValue<string>(3);
                conn.Close();

                PasswordHasher<string> passwordHasher = new();
                PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(loginModel.Email, hashedPassword, loginModel.Password);

                if (passwordVerificationResult == PasswordVerificationResult.Success)
                {
                    string email = loginModel.Email;
                    string password = loginModel.Password;
                    return RedirectToAction("Index", "Home", new { email, password });
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Invalid Credentials. Please try again.");
                }                          
            }
            return View();
        }

        [HttpPost]
        public IActionResult Signup(SignupModel signupModel)
        {
            if(ModelState.IsValid)
            {
                //string connectionConfig = "Data Source=(local);uid=sa;pwd=sql;Initial Catalog=test";
                string connectionConfig = "Server = localhost; Database = dotnet_mvc; Integrated Security = True; ";

                SqlConnection conn = new (connectionConfig);
                string sql = "INSERT INTO users values(@FirstName, @LastName, @Email, @Password, @Phone, @Address)";
                SqlCommand cmd = new (sql, conn);

                PasswordHasher<string> passwordHasher = new();

                string hashedPassword = passwordHasher.HashPassword(signupModel.Email, signupModel.Password);
                
                cmd.Parameters.AddWithValue("@FirstName", signupModel.FirstName);
                cmd.Parameters.AddWithValue("@LastName", signupModel.LastName);
                cmd.Parameters.AddWithValue("@Email", signupModel.Email);
                cmd.Parameters.AddWithValue("@Password", hashedPassword);
                cmd.Parameters.AddWithValue("@Phone", signupModel.Phone);
                cmd.Parameters.AddWithValue("@Address", signupModel.Address);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                string email = signupModel.Email;
                string password = signupModel.Password;

                return RedirectToAction("Index", "Home", new { email, password });

            }
            return View();
        }

    }
}
