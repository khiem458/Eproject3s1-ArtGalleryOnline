﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace ArtGalleryOnline.Controllers
{
    [Authorize(Roles = "admin")]
    public class ArtWorksController : Controller
    {
        private readonly ArtgalleryDbContext _context;

        public ArtWorksController(ArtgalleryDbContext context)
        {
            _context = context;
        }

        // GET: ArtWorks
        public async Task<IActionResult> Index()

        {
            var artWorks = await _context.ArtWorks
                                            .Include(a => a.AuthorArtWork) // Bao gồm thông tin về tác giả tác phẩm (AuthorArtWork)
                                             .Include(a => a.Category) // Bao gồm thông tin về danh mục tác phẩm (Category)
                                            .ToListAsync();
            
            return View(artWorks);
        }
        
        
        
        // GET: ArtWorks/Details/5
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

        // GET: ArtWorks/Create
        public IActionResult Create()
        {
            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist");
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription");
            return View();
        }


        // POST: ArtWorks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArtId,ArtName,ArtDescription,ArtImage,ArtPrice,AuthId,CategoryId,StockQuantity")] ArtWork artWork, IFormFile artImage)
        {
            if (ModelState.IsValid)
            {
                string fileName = Path.GetFileName(artImage.FileName);
                string file_path = Path.Combine(Directory.GetCurrentDirectory(),
                    @"wwwroot/Images", fileName);
                using (var stream = new FileStream(file_path, FileMode.Create))
                {
                    await artImage.CopyToAsync(stream);
                }
                artWork.ArtImage = "Images/" + fileName;

                _context.Add(artWork);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AuthId"] = new SelectList(_context.AuthorArtWork, "AuthId", "Artist", artWork.AuthId);
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryDescription", artWork.CategoryId);
            return View(artWork);

        }


        // GET: ArtWorks/Edit/5
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

        // POST: ArtWorks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ArtId,ArtName,ArtDescription,ArtImage,ArtPrice,AuthId,CategoryId,StockQuantity")] ArtWork artWork, IFormFile artImage)
        {
            if (id != artWork.ArtId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if ( artImage != null)
                    {
                        string fileName = Path.GetFileName(artImage.FileName);
                        string file_path = Path.Combine(Directory.GetCurrentDirectory(),
                            @"wwwroot/Images", fileName);
                        using (var stream = new FileStream(file_path, FileMode.Create))
                        {
                            await artImage.CopyToAsync(stream);
                        }
                        artWork.ArtImage = "Images/" + fileName;
                    }
                    else
                    {
                        // Nếu người dùng không chọn hình ảnh mới, giữ nguyên ảnh cũ
                        var existingArtWork = await _context.ArtWorks.AsNoTracking().FirstOrDefaultAsync(a => a.ArtId == id);
                        artWork.ArtImage = existingArtWork.ArtImage;
                    }

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

        // GET: ArtWorks/Delete/5
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

        // POST: ArtWorks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ArtWorks == null)
            {
                return Problem("Entity set 'ArtgalleryDbContext.ArtWork'  is null.");
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
