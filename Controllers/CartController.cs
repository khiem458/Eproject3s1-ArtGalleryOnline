using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
<<<<<<< HEAD
using Microsoft.EntityFrameworkCore;
=======
>>>>>>> 37403b2cf1321335730aaed83669456188fe7316

namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {
<<<<<<< HEAD
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
=======
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
>>>>>>> 37403b2cf1321335730aaed83669456188fe7316
        }
    }
}
