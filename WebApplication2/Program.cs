using Microsoft.EntityFrameworkCore;
using WebApplication2.Data;

// initialize new instance of web application builder class
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// allow application to handle incoming HTTP requests and render HTML views.
builder.Services.AddControllersWithViews();
// connect database with project with context
builder.Services.AddDbContext<WebApplication2Context>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionString")));


// compile the app and build the application instance.
var app = builder.Build();

// Configure the HTTP request pipeline.
// if the app is not in the development environment, set up and exception to redirect users to home page
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // when an unhandled exception occurs, enable strict transport security to enforce secure https. 
    app.UseHsts();
}

// ensure HTTP requests redirected to https 
app.UseHttpsRedirection();
// enable routing which allows the app to match incoming requests to the endpoints.
app.UseRouting();

// authorizing users to access secured resources
app.UseAuthorization();

// enable serving static files like images, css & javaspript from www root folder
app.MapStaticAssets();

// configure default route redirected when we start application
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
