using System;
using dotnet_mvc.Data;
using Microsoft.AspNetCore.Identity;
using dotnet_mvc.Models;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;

namespace dotnet_mvc.Services
{
	public class AuthService
	{
		private DatabaseContext _db;
        private UserService _userService;
        private readonly IConfiguration _configuration;

        public AuthService(DatabaseContext db, UserService userService, IConfiguration configuration)
		{
			this._db = db;
            this._userService = userService;
            this._configuration = configuration;
        }

        public string Login(LoginModel loginModel) {
            var user = this._userService.findByEmail(loginModel.Email);

            if (user == null)
                throw new SystemException("Invalid credentials");

            PasswordHasher<string> passwordHasher = new();
            PasswordVerificationResult passwordVerificationResult = passwordHasher.VerifyHashedPassword(loginModel.Email, user.Password, loginModel.Password);

            if (passwordVerificationResult == PasswordVerificationResult.Success)
            {
                var key = this._configuration["Jwt:key"];
                var issuer = this._configuration["Jwt:Issuer"];

                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key??""));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Email,user.Email),
                    new Claim(JwtRegisteredClaimNames.Name, user.FirstName),
                    new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString())
                };
                var token = new JwtSecurityToken(
                  issuer,
                  issuer,
                  claims,
                  expires: DateTime.Now.AddMinutes(120),
                  signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            throw new SystemException("Invalid credentials");     
        }

        public void Signup(SignupModel signupModel)
        {

            var user = this._userService.findByEmail(signupModel.Email);

            if (user != null) throw new SystemException("User with the email already exists.");

            PasswordHasher<string> passwordHasher = new();
            string hashedPassword = passwordHasher.HashPassword(signupModel.Email, signupModel.Password);

            UserModel userModel = new UserModel {
                FirstName=signupModel.FirstName,
                LastName = signupModel.LastName,
                Email=signupModel.Email,
                Address = signupModel.Address,
                Password= hashedPassword,
                Phone = signupModel.Phone
            };
            this._userService.create(userModel);
        }
	}
}

