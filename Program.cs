var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
var app = builder.Build();

app.UseExceptionHandler("/Home/ErrorPage");
app.UseStatusCodePagesWithReExecute("/Home/PageNotFound");
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoints => endpoints.MapControllerRoute(name: "default", pattern: "{controller}/{action}/{id?}"));

app.Run();
