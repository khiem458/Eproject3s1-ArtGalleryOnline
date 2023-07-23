using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;


namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {

        private readonly ArtgalleryDbContext _context;

        public CartController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy giỏ hàng từ Session
            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
                HttpContext.Session.Set("Cart", cart);
            }

            return View(cart);
        }
        public IActionResult AddToCart(int artId, int quantity)
        {
            // Lấy thông tin sản phẩm từ database (tùy thuộc vào cách bạn truy xuất dữ liệu)
            var artWork = _context.ArtWorks.FirstOrDefault(a => a.ArtId == artId);
            if (artWork == null)
            {
                // Xử lý nếu không tìm thấy sản phẩm
                return NotFound();
            }

            var cart = HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
            {
                cart = new Cart();
            }

            cart.AddItem(artWork, quantity);

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index");

        }
    }
}
