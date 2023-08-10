using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Controllers
{
    public class OrderDetailsUser: ViewComponent
    {
        private readonly ArtgalleryDbContext _context;

        public OrderDetailsUser(ArtgalleryDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var artgalleryDbContext = _context.OrderDetails.Include(o => o.ArtWork).Include(o => o.Orders)
                 .ToList();
            return View("OrderDetailsUser", artgalleryDbContext);
        }
    }
}
