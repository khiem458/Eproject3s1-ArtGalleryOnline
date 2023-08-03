using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Xml.Linq;

namespace ArtGalleryOnline.Controllers
{
    [ViewComponent(Name = "OrderUser")]
    public class OrderUser: ViewComponent
    {
        private readonly ArtgalleryDbContext _context;

        public OrderUser(ArtgalleryDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var orders =  _context.Orders
                 .Include(o => o.User)
                 .FirstOrDefault();
            return View( "OrderUser",orders);
        }
    }
}
