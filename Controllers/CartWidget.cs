using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Controllers
{
    public class CartWidget: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
           
            return View(HttpContext.Session.GetJson<Cart>("cart"));
        }
    }
}
