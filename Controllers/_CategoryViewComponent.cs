using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace ArtGalleryOnline.Controllers
{
    [ViewComponent(Name = "Category")]
    public class _CategoryViewComponent:ViewComponent
    {
        private readonly ArtgalleryDbContext _context;

        public _CategoryViewComponent(ArtgalleryDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var _category = _context.Categories.ToList();
            return View("_Category", _category);
        }
    }
}
