using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArtGalleryOnline.Controllers
{
	[Authorize(Roles = "author")]
	public class ViewAuthorCreateWorkController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public ViewAuthorCreateWorkController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        // GET: ViewAuthorCreateWork
        public async Task<IActionResult> Index()
        {
            var artgalleryDbContext = _context.ArtWorks.Include(a => a.AuthorArtWork).Include(a => a.Category);
            return View(await artgalleryDbContext.ToListAsync());
        }

        // GET: ViewAuthorCreateWork/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: ViewAuthorCreateWork/Create
        public IActionResult Create()
        {
            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription");
            return View();
        }

        // POST: ViewAuthorCreateWork/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtId,ArtName,ArtDescription,ArtImage,ArtPrice,StockQuantity,AuthId,CategoryId")] ArtWork artWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist", artWork.AuthId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", artWork.CategoryId);
            return View(artWork);
        }

        // GET: ViewAuthorCreateWork/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ArtWorks == null)
            {
                return NotFound();
            }

            var artWork = await _context.ArtWorks.FindAsync(id);
            if (artWork == null)
            {
                return NotFound();
            }
            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist", artWork.AuthId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", artWork.CategoryId);
            return View(artWork);
        }

        // POST: ViewAuthorCreateWork/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtId,ArtName,ArtDescription,ArtImage,ArtPrice,StockQuantity,AuthId,CategoryId")] ArtWork artWork)
        {
            if (id != artWork.ArtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtWorkExists(artWork.ArtId))
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
            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist", artWork.AuthId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", artWork.CategoryId);
            return View(artWork);
        }

        // GET: ViewAuthorCreateWork/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

        // POST: ViewAuthorCreateWork/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArtWorks == null)
            {
                return Problem("Entity set 'ArtgalleryDbContext.ArtWorks'  is null.");
            }
            var artWork = await _context.ArtWorks.FindAsync(id);
            if (artWork != null)
            {
                _context.ArtWorks.Remove(artWork);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtWorkExists(int id)
        {
          return (_context.ArtWorks?.Any(e => e.ArtId == id)).GetValueOrDefault();
        }
    }
}
