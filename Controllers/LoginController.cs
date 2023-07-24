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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Users");
            }
            else
            {

                return View("Login");
            }
        }
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Users users)
        {
            // Check if all required fields are empty
            bool requiredFieldsEmpty = string.IsNullOrEmpty(users.UserName)
                || string.IsNullOrEmpty(users.UserEmail)
                || string.IsNullOrEmpty(users.UserPhoneNum)
                || string.IsNullOrEmpty(users.UserPassword);

            if (ModelState.IsValid && !requiredFieldsEmpty)
            {
                var check = _context.Users.FirstOrDefault(s => s.UserEmail == users.UserEmail);
                if (check == null)
                {
                    users.UserPassword = BCrypt.Net.BCrypt.HashPassword(users.UserPassword);

                    // Set any other properties of the Users object here, if needed
                    users.UserFullName = "";
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
                // If the ModelState is invalid or required fields are empty, set the error message
                ViewBag.Registererr = 0;
                ModelState.AddModelError("", "Please fill in all required fields.");
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

            if (string.IsNullOrEmpty(_userPage.UserEmail) || string.IsNullOrEmpty(_userPage.UserPassword))
            {
                ViewBag.LoginStatus = 0;
                return View();
            }


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
                    return RedirectToAction("Index", "Users");
                }
            }
        }


        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
            Response.Headers["Pragma"] = "no-cache";
            Response.Headers["Expires"] = "0";


            return RedirectToAction("Index", "Home");
        }


        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
