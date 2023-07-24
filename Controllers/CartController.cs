using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public CartController(ArtgalleryDbContext context)
        {
            _context = context;

        }

        public Cart? Cart { get; set; }
        public IActionResult AddToCart(int artId)
        {
            ArtWork? artWork = _context.ArtWorks.FirstOrDefault(p => p.ArtId == artId);
            if (artWork != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                Cart.AddItem(artWork, 1);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View(Cart);
        }
        public IActionResult UpdateCart(int artId)
        {
            ArtWork? artWork = _context.ArtWorks.FirstOrDefault(p => p.ArtId == artId);
            if (artWork != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                if (Cart.CartItems.Any(item => item.ArtWork.ArtId == artId && item.Quantity > 1))
                {
                    Cart.AddItem(artWork, -1);
                    HttpContext.Session.SetJson("cart", Cart);
                }
            }
            return View("AddToCart", Cart);
        }
        public IActionResult RemoveFromcart(int artId)
        {
            ArtWork? artWork = _context.ArtWorks.FirstOrDefault(p => p.ArtId == artId);
            if (artWork != null)
            {
                Cart = HttpContext.Session.GetJson<Cart>("cart");
                Cart.RemoveItem(artWork);
                HttpContext.Session.SetJson("cart", Cart);
            }
            return View("AddToCart", Cart);
        }
        public IActionResult Index()
        {
            return View("AddToCart", HttpContext.Session.GetJson<Cart>("cart"));
        }
       

       


    }
}
