using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ArtGalleryOnline.Models;
using ArtGalleryOnline.ModelsView;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;

namespace ArtGalleryOnline.Controllers
{
    [Authorize(Roles = "user")]
    public class UsersController : Controller
    {
        private readonly ArtgalleryDbContext _context;



        public UsersController(ArtgalleryDbContext context)
        {
            _context = context;

        }
        // GET: Users
        public async Task<IActionResult> Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var users = await _context.Users.ToListAsync();
                var artworks = await _context.ArtWorks.ToListAsync();

                var viewModel = new UserArtworkViewModels
                {
                    Users = users,
                    ArtWorks = artworks
                };

                return View(viewModel);
            }
            else
            {

                return RedirectToAction("Login", "Login");
            }

        }


        // GET: Users/Details/5
        public async Task<IActionResult> UserDetails(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (users == null)
            {
                return NotFound();
            }

            return View("UserDetails",users);
        }

        //bắt đầu order
        public async Task<IActionResult> OrderUser(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View("OrderUser", orders);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }
        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orders == null)
            {
                return NotFound();
            }

            var orders = await _context.Orders
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (orders == null)
            {
                return NotFound();
            }

            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orders == null)
            {
                return Problem("Entity set 'ArtgalleryDbContext.Orders'  is null.");
            }
            var orders = await _context.Orders.FindAsync(id);
            if (orders != null)
            {
                _context.Orders.Remove(orders);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //hêt Order


        public IActionResult Store(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                var artWork = _context.ArtWorks.Where(a => a.ArtName.Contains(searchString));
                return View(artWork.ToList());
            }
            var artWorks = _context.ArtWorks
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


        // GET: Users1/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var users = await _context.Users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }
            return View(users);
        }

        // POST: Users1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,UserFullName,UserEmail,UserGender,UserAge,UserPhoneNum,UserAddress,UserPassword,UserRole,RememberMe,IsVerified")] Users users)
        {
            if (id != users.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(users);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsersExists(users.UserId))
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
            return View(users);
        }     
       
        private bool UsersExists(int id)
        {
            return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
