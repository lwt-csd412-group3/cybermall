using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberMall.Data;
using CyberMall.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Identity;

namespace CyberMall.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        public ItemController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var itemListings = await _context.ItemListings.Where(il => il.Seller == currentUser).ToListAsync();
            return View(itemListings);
        }

        // GET: Item/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemListings
                .FirstOrDefaultAsync(m => m.ItemListingId == id);
            if (item == null)
            {
                return NotFound();
            }

            return View(item);
        }

        // GET: Item/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Item/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemListing item, IFormFile imageFile)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            item.Seller = currentUser;
            //item.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in currentUser's ID

            if (ModelState.IsValid)
            {
                // Check if a file was uploaded
                if (imageFile != null && imageFile.Length > 0)
                {
                    // Convert the uploaded image to a byte array
                    using (var memoryStream = new MemoryStream())
                    {
                        await imageFile.CopyToAsync(memoryStream);
                        item.ImageData = memoryStream.ToArray();
                    }
                }

                // Add the item to the database
                _context.Add(item);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(item);
        }

        // GET: Item/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemListings.FirstOrDefaultAsync(il => il.ItemListingId == id);

            if (item == null)
            {
                return NotFound();
            }

            var currentUser = await _userManager.GetUserAsync(User);

            if (item.Seller != currentUser)
            {
                return Forbid(); // Deny access if not the owner
            }
            return View(item);
        }

        // POST: Item/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ItemListing item, IFormFile imageFile)
        { 
            if (id != item.ItemListingId)
            {
                return NotFound();
            }

            // Fetch the existing item from the database to retrieve the original SellerId
            var existingItem = await _context.ItemListings.FirstOrDefaultAsync(il => il.ItemListingId == id);

            var currentUser = await _userManager.GetUserAsync(User);

            if (existingItem == null)
            {
                return NotFound();
            }

            if (existingItem.Seller != currentUser)
            {
                return Forbid(); // Prevent unauthorized editing
            }



            _context.Entry(existingItem).CurrentValues.SetValues(item);

            // preserve seller information
            item.Seller = currentUser;

            if (ModelState.IsValid)
            {
                try
                {
                    // If a new image file is uploaded, update the ImageData; otherwise, keep the existing image
                    if (imageFile != null && imageFile.Length > 0)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            await imageFile.CopyToAsync(memoryStream);
                            existingItem.ImageData = memoryStream.ToArray(); // Update ImageData with the uploaded file
                        }
                    }
                    //_context.Update(item);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemExists(item.ItemListingId))
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
            return View(item);
        }

        // GET: Item/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var item = await _context.ItemListings.FirstOrDefaultAsync(il => il.ItemListingId == id);

            var currentUser = await _userManager.GetUserAsync(User);

            if (item == null)
            {
                return NotFound();
            }

            if (item.Seller != currentUser)
            {
                return Forbid(); // Ensure only the owner can delete
            }

            return View(item);
        }

        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.ItemListings.FindAsync(id);

            var currentUser = await _userManager.GetUserAsync(User);

            if (item == null)
            {
                return NotFound();
            }

            if (item.Seller != currentUser)
            {
                return Forbid(); // Ensure only the owner can delete
            }
            _context.ItemListings.Remove(item);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemExists(int id)
        {
            return _context.ItemListings.Any(e => e.ItemListingId == id);
        }
    }
}
