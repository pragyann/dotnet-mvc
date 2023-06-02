using System.Text;
using dotnet_mvc.Data;
using dotnet_mvc.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
configuration.SetBasePath(Directory.GetCurrentDirectory());
configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Registering Services
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<DatabaseContext>();
builder.Services.AddSession();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "Nirdosh",
        ValidAudience = "College",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("dafadfjhafk123jk2"))
    };
});

// App 
var app = builder.Build();
app.UseAuthentication();
app.UseExceptionHandler("/Home/ErrorPage");
app.UseStatusCodePagesWithReExecute("/Home/PageNotFound");
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}"));

app.Run();
