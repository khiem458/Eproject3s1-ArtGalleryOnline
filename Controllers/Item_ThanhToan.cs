using ArtGalleryOnline.Infrastructure;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArtGalleryOnline.Controllers
{
    public class Item_ThanhToan: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            
            return View("Item_ThanhToan", HttpContext.Session.GetJson<Cart>("cart"));
        }
       
    }
}
