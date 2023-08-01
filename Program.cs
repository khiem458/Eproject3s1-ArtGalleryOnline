using ArtGalleryOnline.Models;
using ArtGalleryOnline.ModelsView;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

var builder = WebApplication.CreateBuilder(args);
<<<<<<< HEAD
=======
builder.Services.AddDbContext<ArtgalleryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));

//email configuration
var emailConfiguration = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfiguration);
>>>>>>> e09e7c642eee55ebfeec1bdb767a265dc3217203

// Add services to the container.
builder.Services.AddDbContext<ArtgalleryDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("mycon")));
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.MaxAge = TimeSpan.FromDays(30);
        options.SlidingExpiration = true; // Enables "Remember Me" functionality with sliding expiration
        options.LoginPath = "/Login/Login"; // The login path if authentication is required
        options.LogoutPath = "/Login/Logout"; // The logout path
        options.AccessDeniedPath = "/Error/AccessDenied";
    });
PaypalConfiguration.Configure(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseSession();

app.UseAuthentication(); // Add this before app.UseAuthorization();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
