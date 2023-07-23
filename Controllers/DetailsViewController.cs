using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtGalleryOnline.Controllers
{
    
    public class DetailsViewController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public DetailsViewController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> ArtWorkByCategory(int CatId)

        {
            var artWorks = await _context.ArtWork
                                            .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                             .Include(a => a.Category) // Bao gồm thông tin về danh mục tác phẩm (Category)
                                             .Where(a => a.CategoryId == CatId)
                                            .ToListAsync();

            return View(artWorks);
        }
        public async Task<IActionResult> ArtWorkDetails(int? id)
        {
            if (id == null || _context.ArtWork == null)
            {
                return NotFound();
            }

            var artWork = await _context.ArtWork
                .Include(a => a.AuthorArtWork)
                .Include(a => a.Category)
                .FirstOrDefaultAsync(m => m.ArtId == id);
            if (artWork == null)
            {
                return NotFound();
            }

            return View(artWork);
        }
        public async Task<IActionResult> BlogDetails(int? id)
        {
            if (id == null || _context.Blog == null)
            {
                return NotFound();
            }

            var blog = await _context.Blog
                .Include(b => b.AuthorArtWork)
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }
    }
}
