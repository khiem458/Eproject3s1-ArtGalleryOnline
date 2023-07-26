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
            var artWorks = await _context.ArtWorks
                                            .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                             .Include(a => a.Category) // Bao gồm thông tin về danh mục tác phẩm (Category)
                                             .Where(a => a.CategoryId == CatId)
                                            .ToListAsync();

            return View(artWorks);
        }
        public async Task<IActionResult> ArtWorkDetails(int? id)
        {
            if (id == null || _context.ArtWorks == null)
            {
                return NotFound();
            }

            var artWork = await _context.ArtWorks
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
                .Include(b => b.CommentBlogs)
                .FirstOrDefaultAsync(m => m.BlogId == id);

            //var solidBlog =await  _context.Blog.FirstOrDefaultAsync(m => m.BlogId == id);
            //var solidAuthorArtWork =await _context.AuthorArtWork.FirstOrDefaultAsync(x => x.AuthId == solidBlog!.AuthId);
            //var comment = await _context.CommentBlog.FirstOrDefaultAsync(x => x.BlogId == solidBlog!.BlogId);

            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        public async Task<IActionResult> PostComment(int blogId,string name,string email,string message)
        {
            var blog =await _context.Blog.FirstOrDefaultAsync(x => x.BlogId == blogId);
            if(blog!=null)
            {
                var newComment = _context.CommentBlog.Add(new CommentBlog
                {
                    BlogId = blogId,
                    Name = name,
                    Email = email,
                    Message = message,
                    DatePosted = DateTime.Now,
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("BlogDetails",new {id = blogId });
        }
    }
}
