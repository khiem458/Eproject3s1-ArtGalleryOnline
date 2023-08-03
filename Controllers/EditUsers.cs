using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ArtGalleryOnline.Controllers
{
    [ViewComponent(Name = "EditUsers")]
    public class EditUsers: ViewComponent
    {
        private readonly ArtgalleryDbContext _context;

        public EditUsers(ArtgalleryDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var EditUser = _context.Users.FirstOrDefault();
            return View("EditUsers",EditUser);          
        }
        private Users GetCurrentUser()
        {
            // Lấy ID của người dùng đăng nhập từ Claims
            var userId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Truy vấn người dùng đăng nhập từ CSDL bằng ID
            return _context.Users.FirstOrDefault(u => u.UserId == int.Parse(userId));
        }
    }
}
