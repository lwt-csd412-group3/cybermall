﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CyberMall.Data;
using CyberMall.Models;
using Microsoft.AspNetCore.Identity;

namespace CyberMall.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;

        // Constructor to inject dependencies
        public CartController(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        // Add an item to the cart (this is a GET method for linking)
        public async Task<IActionResult> AddToCart(int itemListingId)
        {
            int quantity = 1; // Default quantity is set to 1

            // Fetch the ItemListing from the database
            var itemListing = await _dbContext.ItemListings.FindAsync(itemListingId);

            if (itemListing == null)
            {
                return NotFound(); // Return 404 if the item is not found
            }

            // Get the current user (assuming they are logged in)
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Check if the user already has this item in their cart
            var existingItemSale = _dbContext.ItemSale
                .FirstOrDefault(i => i.ItemListingId == itemListingId && i.UserId == user.Id);

            if (existingItemSale != null)
            {
                // If the item already exists in the cart, increase the quantity
                existingItemSale.Quantity += quantity;
                _dbContext.ItemSale.Update(existingItemSale);
            }
            else
            {
                // Create a new cart item for the user
                var itemSale = new ItemSale
                {
                    ItemListingId = itemListing.ItemListingId,
                    ItemListing = itemListing, // Link the ItemSale to ItemListing
                    Price = itemListing.Price, // Store the price at the time of the sale
                    Quantity = quantity,
                    Discount = 0, // Set discount logic if needed
                    UserId = user?.Id // Associate with the current logged-in user
                };

                _dbContext.ItemSale.Add(itemSale);
            }

            // Save changes to the database
            await _dbContext.SaveChangesAsync();

            // Redirect to the cart view (or wherever you want to show the updated cart)
            return RedirectToAction("ViewCart");
        }

        // View the items in the cart
        public async Task<IActionResult> ViewCart()
        {
            // Get the current user (assuming they are logged in)
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Fetch the user's cart items from the database
            var cartItems = _dbContext.ItemSale
                                      .Include(i => i.ItemListing) // Include related ItemListing data
                                      .Where(i => i.UserId == user.Id)
                                      .ToList();

            return View(cartItems); // Return the list of cart items to the view
        }
        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int itemListingId)
        {
            // Get the current user
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Find the cart item for the user and itemListingId
            var itemSale = await _dbContext.ItemSale
                .FirstOrDefaultAsync(i => i.ItemListingId == itemListingId && i.UserId == user.Id);

            if (itemSale == null)
            {
                return NotFound(); // Return 404 if the item is not found
            }

            // Remove the item from the cart
            _dbContext.ItemSale.Remove(itemSale);
            await _dbContext.SaveChangesAsync();

            // Redirect back to the cart page
            return RedirectToAction("ViewCart");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateCart(Dictionary<int, int> quantities)
        {
            // Get the current user (assuming they are logged in)
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            foreach (var itemSale in _dbContext.ItemSale.Where(i => i.UserId == user.Id))
            {
                if (quantities.ContainsKey(itemSale.Id))
                {
                    // Update the quantity of the item in the cart
                    itemSale.Quantity = quantities[itemSale.Id];
                    _dbContext.ItemSale.Update(itemSale);
                }
            }

            // Save the changes to the database
            await _dbContext.SaveChangesAsync();

            // Redirect back to the cart view
            return RedirectToAction("ViewCart");
        }


        // Checkout (a simple placeholder action for the checkout process)
        public IActionResult Checkout()
        {
            return View(); // TODO: implement the actual checkout logic here
        }
    }
}