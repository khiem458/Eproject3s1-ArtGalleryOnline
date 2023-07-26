using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using ArtGalleryOnline.ModelsView;
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
       public IActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CheckOut(OrderViewModel req)
        {
            if (ModelState.IsValid)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                if (cart != null && cart.CartItems.Any()) // Kiểm tra giỏ hàng có sản phẩm không
                {
                    Orders orders = new Orders();
                    orders.RecipientName = req.RecipientName;
                    orders.RecipientPhone = req.RecipientPhone;
                    orders.ShippingAddress = req.ShippingAddress;
                    orders.RecipientEmail = req.RecipientEmail;
                    orders.OrderDetails = new List<OrderDetail>();
                    cart.CartItems.ForEach(x => orders.OrderDetails.Add(new OrderDetail
                    {
                        ArtId = x.CartItemId,
                        Quantity = x.Quantity,
                        Price = x.Price
                    }));
                    orders.TotalAmount = cart.CartItems.Sum(x => (x.Price * x.Quantity));
                    orders.TypePayment = req.TypePayment;
                    orders.CreatedDate = DateTime.Now;
                    orders.ModifiedDate = DateTime.Now;
                    orders.CreatedBy = req.RecipientPhone;

                    try
                    {
                        _context.Orders.Add(orders);
                        _context.SaveChanges();
                        return RedirectToAction("CheckOutSuccess");
                    }
                    catch (Exception ex)
                    {
                        // Xử lý lỗi khi lưu đơn hàng vào cơ sở dữ liệu
                        // Ghi log hoặc hiển thị thông báo lỗi tùy ý
                        return View("Error");
                    }
                }
            }

            // Trả về view "CheckOut" nếu không hợp lệ, hoặc giỏ hàng rỗng
            return View("CheckOut");
        }


        public IActionResult CheckOutSuccess()
        {
            return View();
        }


    }
}
