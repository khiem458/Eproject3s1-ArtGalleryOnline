using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace ArtGalleryOnline.Controllers
{
    public class LoginController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public LoginController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        // GET: Login
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.ToListAsync()) :
                        Problem("Entity set 'ArtgalleryDbContext.Users'  is null.");
        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users _userPage)
        {
            var _user = _context.Users.SingleOrDefault(m => m.UserEmail == _userPage.UserEmail && m.UserPassword == _userPage.UserPassword);
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
                return View();
            }
            else
            {
                // Create claims for the authenticated user
                var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, _user.UserEmail),
            new Claim("FullName", _user.UserName),
            new Claim(ClaimTypes.Role, _user.UserRole.ToString()), // UserRole is an enum, so convert it to string
        };

                // Create identity
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Create authentication properties
                var authProperties = new AuthenticationProperties
                {
                    // You can add expiration time, sliding expiration, etc. here if needed
                    // Example: ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                    //          IsPersistent = true,
                };

                // Sign in the user
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // Redirect based on UserRole
                if (_user.UserRole == UserRole.admin)
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
        }
        public async Task<IActionResult> Logout()
        {
            // Perform the sign-out
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect the user to the desired page after logging out
            return RedirectToAction("Index", "Home"); // Redirect to the Home page or any other page you want
        }


        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
