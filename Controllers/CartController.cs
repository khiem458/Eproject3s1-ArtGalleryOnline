using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {
        public Cart? Cart { get; set; }
        private readonly ArtgalleryDbContext _context;

        public CartController(ArtgalleryDbContext context)
        {
            _context = context;
        }

       
        public IActionResult AddToCart(int ArtId)
        {
            ArtWork? artWork = _context.ArtWork.FirstOrDefault(p=>p.ArtId == ArtId);
            if(artWork != null) 
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(artWork, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View(Cart);
        }
    }
}
