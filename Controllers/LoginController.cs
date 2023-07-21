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
using BCrypt.Net;

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
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users users)
        {
            if (ModelState.IsValid)
            {
                var check = _context.Users.FirstOrDefault(s => s.UserEmail == users.UserEmail);
                if (check == null)
                {
                    users.UserPassword = BCrypt.Net.BCrypt.HashPassword(users.UserPassword);

                    // Set any other properties of the Users object here, if needed
                    // For example, you might want to set the UserRole or any other user-specific properties

                    // Add the new user to the database
                    _context.Users.Add(users);
                    _context.SaveChanges();

                    // Redirect the user to the login page after successful registration
                    return RedirectToAction("Login");
                }
                else
                {
                    ModelState.AddModelError("UserEmail", "Email already exists. Please use a different email address.");
                }
            }

            // If the ModelState is invalid or the email already exists, return to the registration page with the provided user data
            return View(users);
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
                    return RedirectToAction("Index", "Admin");
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
