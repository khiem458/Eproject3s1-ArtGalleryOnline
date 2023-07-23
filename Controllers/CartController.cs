using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddToCart(int ArtId)
        {
            return View();
        }
    }
}
