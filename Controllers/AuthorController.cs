﻿using System;
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
    [Authorize(Roles = "admin")]
    public class AuthorController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public AuthorController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        // GET: Author
        public async Task<IActionResult> Index()
        {
              return _context.AuthorArtWork != null ? 
                          View(await _context.AuthorArtWork.ToListAsync()) :
                          Problem("Entity set 'ArtgalleryDbContext.AuthorArtWork'  is null.");
        }

        // GET: Author/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.AuthorArtWork == null)
            {
                return NotFound();
            }

            var authorArtWork = await _context.AuthorArtWork
                .FirstOrDefaultAsync(m => m.AuthId == id);
            if (authorArtWork == null)
            {
                return NotFound();
            }

            return View(authorArtWork);
        }

        // GET: Author/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Author/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthId,Artist,AuthDesciption")] AuthorArtWork authorArtWork)
        {
            if (ModelState.IsValid)
            {
                _context.Add(authorArtWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(authorArtWork);
        }

        // GET: Author/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.AuthorArtWork == null)
            {
                return NotFound();
            }

            var authorArtWork = await _context.AuthorArtWork.FindAsync(id);
            if (authorArtWork == null)
            {
                return NotFound();
            }
            return View(authorArtWork);
        }

        // POST: Author/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthId,Artist,AuthDesciption")] AuthorArtWork authorArtWork)
        {
            if (id != authorArtWork.AuthId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(authorArtWork);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorArtWorkExists(authorArtWork.AuthId))
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
            return View(authorArtWork);
        }

        // GET: Author/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.AuthorArtWork == null)
            {
                return NotFound();
            }

            var authorArtWork = await _context.AuthorArtWork
                .FirstOrDefaultAsync(m => m.AuthId == id);
            if (authorArtWork == null)
            {
                return NotFound();
            }

            return View(authorArtWork);
        }

        // POST: Author/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.AuthorArtWork == null)
            {
                return Problem("Entity set 'ArtgalleryDbContext.AuthorArtWork'  is null.");
            }
            var authorArtWork = await _context.AuthorArtWork.FindAsync(id);
            if (authorArtWork != null)
            {
                _context.AuthorArtWork.Remove(authorArtWork);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorArtWorkExists(int id)
        {
          return (_context.AuthorArtWork?.Any(e => e.AuthId == id)).GetValueOrDefault();
        }
    }
}
