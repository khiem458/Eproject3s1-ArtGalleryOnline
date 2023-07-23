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
                        View("Login") :
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
            // Check if all fields are empty
            bool allFieldsEmpty = string.IsNullOrEmpty(users.UserName)
                && string.IsNullOrEmpty(users.UserFullName)
                && string.IsNullOrEmpty(users.UserEmail)
                && string.IsNullOrEmpty(users.UserPhoneNum)
                && string.IsNullOrEmpty(users.UserPassword);

            if (ModelState.IsValid && !allFieldsEmpty)
            {
                var check = _context.Users.FirstOrDefault(s => s.UserEmail == users.UserEmail);
                if (check == null)
                {
                    users.UserPassword = BCrypt.Net.BCrypt.HashPassword(users.UserPassword);

                    // Set any other properties of the Users object here, if needed
                    users.UserFullName = ""; // Empty string, so it won't be shown in the registration view
                    users.UserAddress = "";

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
            else
            {
                // If the ModelState is invalid or all fields are empty, set the error message
                ViewBag.Registererr = 0;
                ModelState.AddModelError("", "All fields cannot be blank.");
            }

            // Return to the registration page with the provided user data
            return View(users);
        }




        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users _userPage)
        {
            // Check if the required fields are filled
            if (string.IsNullOrEmpty(_userPage.UserEmail) || string.IsNullOrEmpty(_userPage.UserPassword))
            {
                ViewBag.LoginStatus = 0;
                return View();
            }

            // Find the user with the given email in the database
            var _user = _context.Users.SingleOrDefault(m => m.UserEmail == _userPage.UserEmail);

            if (_user == null || !BCrypt.Net.BCrypt.Verify(_userPage.UserPassword, _user.UserPassword))
            {
                // If the user is not found or the password is incorrect, show an error message
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
            new Claim(ClaimTypes.Role, _user.UserRole.ToString()),
        };

               
                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                
                var authProperties = new AuthenticationProperties
                {
                    
                };

                
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

               
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
            return RedirectToAction("Index", "Login"); // Redirect to the Home page or any other page you want
        }


        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
