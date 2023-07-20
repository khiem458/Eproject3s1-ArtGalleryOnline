using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
