using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using ArtGalleryOnline.ModelsView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using static ArtGalleryOnline.Models.Cart;

namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {
        private readonly ArtgalleryDbContext _context;
        private readonly string _ClientId;
        private readonly string _SecretKey;

        public CartController(ArtgalleryDbContext context, IConfiguration config)
        {
            _context = context;
            _ClientId = config["PaypalSetting:ClientId"];
            _SecretKey = config["PaypalSetting:SecretKey"];

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

        public IActionResult CheckOutSuccess()
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


        public ActionResult PaymentWithPaypal(string Cancel = null)
        {


            // Getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                // A resource representing a Payer that funds a payment Payment Method as PayPal  
                // Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Query["PayerID"].ToString();
                if (string.IsNullOrEmpty(payerId))
                {
                    // This section will be executed first because PayerID doesn't exist  
                    // It is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the URL on which PayPal sends back the data.  
                    var guid = Guid.NewGuid().ToString();
                    var baseURI = Url.Action("PaymentWithPaypal", "Cart", new { guid }, Request.Scheme);
                    // Here we are generating a GUID for storing the paymentID received in session  
                    // which will be used in the payment execution  


                    // CreatePayment function gives us the payment approval URL  
                    // on which payer is redirected for PayPal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);

                    // Get links returned from PayPal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            // Saving the PayPal redirect URL to which the user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }

                    // Save the paymentID in the session  
                    HttpContext.Session.SetString("guid", createdPayment.id);

                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    var guid = Request.Query["guid"].ToString();
                    var executedPayment = ExecutePayment(apiContext, payerId, HttpContext.Session.GetString("guid"));
                    // If executed payment failed then we will show payment failure message to the user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("CheckOut");
                    }
                }
            }
            catch (PayPal.PaymentsException ex)
            {
                // Handle PayPal payment exceptions
                return View("CheckOut");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return View("CheckOut");
            }
            // On successful payment, show the success page to the user.  
            return View("CheckOutSuccess");
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            Cart cart = HttpContext.Session.GetJson<Cart>("cart");

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  
            foreach (var item in cart.CartItems)
            {
                itemList.items.Add(new Item()
                {
                    name = item.ArtWork.ArtName,
                    currency = "USD",
                    price = item.ArtWork.ArtPrice.ToString(), // Format the price to 2 decimal places
                    quantity = item.Quantity.ToString(),
                    sku = item.ArtWork.ArtId.ToString(),
                });
            }
            var subtotal = cart.ComputeTotalValues();
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            var total = subtotal + 10 + 10;
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "10",
                shipping = "10",
                subtotal = cart.ComputeTotalValues().ToString()
            };

            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = total.ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details

            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            var paypalOrderId = DateTime.Now.Ticks;
            transactionList.Add(new Transaction()
            {
                description = $"Invoice #{paypalOrderId}",
                invoice_number = paypalOrderId.ToString(), //Generate an Invoice No    
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };

            //mua thanh cong xoa gio hang
            HttpContext.Session.Remove("cart");
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }





    }
}
