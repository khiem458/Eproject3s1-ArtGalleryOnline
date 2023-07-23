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

        public async Task<IActionResult> Index()
        {
            var artWorks = await _context.ArtWorks
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                            .Include(a => a.Category) // Bao gồm thông tin về danh mục tác phẩm (Category)
                                            .ToArrayAsync();
            return View(artWorks);
        }

        public async Task<IActionResult> Store(string searchString)
        {
            IQueryable<ArtWork> query = _context.ArtWorks
                                           .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                           .Include(a => a.Category); // Bao gồm thông tin về danh mục tác phẩm (Category)

            if (!String.IsNullOrEmpty(searchString))
            {
                query = query.Where(a => a.ArtName.Contains(searchString));
            }

            var artWorks = await query.ToListAsync();
            return View(artWorks);
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