using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ArtGalleryOnline.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ArtgalleryDbContext _context;

        public HomeController(ILogger<HomeController> logger, ArtgalleryDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var artWorks = _context.ArtWork
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                            .Include(a => a.Category); // Bao gồm thông tin về danh mục tác phẩm (Category)
                                          
            return View(artWorks.ToList());
        }

        public IActionResult Store(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var artWork = _context.ArtWork.Where(a => a.ArtName.Contains(searchString));
                return View(artWork.ToList());
            }
            var artWorks = _context.ArtWork
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                            .Include(a => a.Category); // Bao gồm thông tin về danh mục tác phẩm (Category)

            return View(artWorks.ToList());
        }
        public IActionResult About()
        {
            return View();
        }
        public IActionResult Gallery()
        {
            return View();
        }
        public IActionResult Blog()
        {
            var Blog = _context.Blog.Include(b => b.AuthorArtWork);
            return View(Blog.ToList());
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}