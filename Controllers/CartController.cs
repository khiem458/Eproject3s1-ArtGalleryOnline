using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using ArtGalleryOnline.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        public IActionResult CheckOut(Orders orders)
        {
            if (ModelState.IsValid)
            {
                Cart cart = HttpContext.Session.GetJson<Cart>("cart") ?? new Cart();
                if (cart != null && cart.CartItems.Any()) // Kiểm tra giỏ hàng có sản phẩm không
                {
                    // Thiết lập các thông tin đơn hàng từ đối tượng OrderViewModel req

                    orders.OrderDate = DateTime.Now;
                    orders.RequestStatus = RequestStatus.Pending;
                    orders.Quantity = cart.CartItems.Count;
                    orders.TotalAmount = cart.CartItems.Sum(e => e.ArtWork.ArtPrice * e.Quantity);
                    var claim = ((ClaimsIdentity)User.Identity).FindFirst("UserId");
                    var userId = claim == null ? null : claim.Value;
                    orders.UserId = int.Parse(userId);
                    


                    // Lưu đơn hàng vào cơ sở dữ liệu
                    _context.Orders.Add(orders);
                    _context.SaveChanges();


                    // Lưu thông tin chi tiết đơn hàng vào bảng OrderDetails
                    foreach (var cartItem in cart.CartItems)
                        {
                            OrderDetail orderDetail = new OrderDetail
                            {
                                OrderId = orders.OrderId,
                                ArtId = cartItem.ArtWork.ArtId,
                                Quantity = cartItem.Quantity,
                                Price = cartItem.Price
                            };
                            _context.OrderDetails.Add(orderDetail);
                        }
                   
                    _context.SaveChanges();

                        // Xóa giỏ hàng sau khi lưu đơn hàng thành công
                        HttpContext.Session.Remove("cart");

                        // Chuyển hướng đến trang hiển thị thông báo thành công
                        return RedirectToAction("CheckOutSuccess");
                   
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
