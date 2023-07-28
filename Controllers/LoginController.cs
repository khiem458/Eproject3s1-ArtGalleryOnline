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
        public IActionResult RegisterCreate()
        {
            return View();
        }

        // POST: ManageUsers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterCreate([Bind("UserId,UserName,UserFullName,UserEmail,UserGender,UserAge,UserPhoneNum,UserAddress,UserPassword,UserRole")] Users users)
        {
            if (ModelState.IsValid)
            {
                _context.Add(users);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(users);
        }        
        public async Task<IActionResult> Login()
        {
           
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Users _userPage)
        {
            var _user = _context.Users.Where(m => m.UserEmail == _userPage.UserEmail && m.UserPassword == _userPage.UserPassword).FirstOrDefault();
            if (_user == null)
            {
                ViewBag.LoginStatus = 0;
                return View();
            }
            else
            {
                var claims = new List<Claim>
                {
                        new Claim(ClaimTypes.Name, _user.UserEmail),
                         new Claim("UserId", _user.UserId.ToString()),
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

            // Nếu không có vai trò nào phù hợp hoặc có lỗi xảy ra, quay lại trang đăng nhập
            return View();
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
