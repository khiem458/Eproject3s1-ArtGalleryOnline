using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Controllers
{
    [Authorize(Roles ="admin")]
    public class AdminController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public AdminController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var artgalleryDbContext = _context.Orders.Include(o => o.User);
            return View(await artgalleryDbContext.ToListAsync());
        }
        
    }
}
