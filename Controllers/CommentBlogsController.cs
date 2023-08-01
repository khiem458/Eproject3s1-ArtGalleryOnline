using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;

namespace ArtGalleryOnline.Controllers
{
    public class CommentBlogsController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public CommentBlogsController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        // GET: CommentBlogs
        public async Task<IActionResult> Index()
        {
            var artgalleryDbContext = _context.CommentBlog.Include(c => c.Blog);
            return View(await artgalleryDbContext.ToListAsync());
        }

        // GET: CommentBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CommentBlog == null)
            {
                return NotFound();
            }

            var commentBlog = await _context.CommentBlog
                .Include(c => c.Blog)
                .FirstOrDefaultAsync(m => m.CommentBlogId == id);
            if (commentBlog == null)
            {
                return NotFound();
            }

            return View(commentBlog);
        }

        // GET: CommentBlogs/Create
        public IActionResult Create()
        {
            ViewData["BlogId"] = new SelectList(_context.Blog, "BlogId", "Description");
            return View();
        }

        // POST: CommentBlogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CommentBlogId,BlogId,Name,Email,DatePosted,Message")] CommentBlog commentBlog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(commentBlog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blog, "BlogId", "Description", commentBlog.BlogId);
            return View(commentBlog);
        }

        // GET: CommentBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CommentBlog == null)
            {
                return NotFound();
            }

            var commentBlog = await _context.CommentBlog.FindAsync(id);
            if (commentBlog == null)
            {
                return NotFound();
            }
            ViewData["BlogId"] = new SelectList(_context.Blog, "BlogId", "Description", commentBlog.BlogId);
            return View(commentBlog);
        }

        // POST: CommentBlogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CommentBlogId,BlogId,Name,Email,DatePosted,Message")] CommentBlog commentBlog)
        {
            if (id != commentBlog.CommentBlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(commentBlog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentBlogExists(commentBlog.CommentBlogId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BlogId"] = new SelectList(_context.Blog, "BlogId", "Description", commentBlog.BlogId);
            return View(commentBlog);
        }

        // GET: CommentBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CommentBlog == null)
            {
                return NotFound();
            }

            var commentBlog = await _context.CommentBlog
                .Include(c => c.Blog)
                .FirstOrDefaultAsync(m => m.CommentBlogId == id);
            if (commentBlog == null)
            {
                return NotFound();
            }

            return View(commentBlog);
        }

        // POST: CommentBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CommentBlog == null)
            {
                return Problem("Entity set 'ArtgalleryDbContext.CommentBlog'  is null.");
            }
            var commentBlog = await _context.CommentBlog.FindAsync(id);
            if (commentBlog != null)
            {
                _context.CommentBlog.Remove(commentBlog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentBlogExists(int id)
        {
          return (_context.CommentBlog?.Any(e => e.CommentBlogId == id)).GetValueOrDefault();
        }
    }
}
