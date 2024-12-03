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

namespace CyberMall.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Item
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemListings.ToListAsync());
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
            item.SellerId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user's ID

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

            var item = await _context.ItemListings.AsNoTracking().FirstOrDefaultAsync(il => il.ItemListingId == id);

            if (item == null)
            {
                return NotFound();
            }

            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (item.SellerId != currentUserId)
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
            var existingItem = await _context.ItemListings.AsNoTracking().FirstOrDefaultAsync(il => il.ItemListingId == id);

            if (existingItem == null)
            {
                return NotFound();
            }

            if (existingItem.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid(); // Prevent unauthorized editing
            }

            // Preserve the SellerId from the existing item
            item.SellerId = existingItem.SellerId;

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
                            item.ImageData = memoryStream.ToArray(); // Update ImageData with the uploaded file
                        }
                    }

                    else
                    {
                        item.ImageData = existingItem.ImageData; // Retain the existing image if no new file is uploaded
                    }
                    _context.Update(item);
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

            var item = await _context.ItemListings.AsNoTracking().FirstOrDefaultAsync(il => il.ItemListingId == id);

            if (item == null)
            {
                return NotFound();
            }

            if (item.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
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

            if (item == null)
            {
                return NotFound();
            }

            if (item.SellerId != User.FindFirstValue(ClaimTypes.NameIdentifier))
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
