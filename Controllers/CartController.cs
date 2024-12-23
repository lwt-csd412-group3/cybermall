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
using Castle.Core.Internal;

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
        public async Task<IActionResult> AddToCart(long itemListingId)
        {
            int quantity = 1; // Default quantity is set to 1

            // Fetch the ItemListing from the database
            var itemListing = await _dbContext.ItemListings.FindAsync(itemListingId);

            if (itemListing == null)
            {
                return NotFound(); // Return 404 if the item is not found
            }

            // Get the current user (assuming they are logged in)
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Check if the user already has this item in their cart
            var existingItemSale = user.ItemsInCart.Find(i => i.ItemListing.Id == itemListingId);

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
                    ItemListing = itemListing, // Link the ItemSale to ItemListing
                    Price = itemListing.Price, // Store the price at the time of the sale
                    Quantity = quantity,
                    Discount = 0, // Set discount logic if needed
                    User = user
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
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Fetch the user's cart items from the database
            var cartItems = user.ItemsInCart;

            return View(cartItems); // Return the list of cart items to the view
        }
        // Remove an item from the cart
        [HttpPost]
        public async Task<IActionResult> RemoveFromCart(int itemListingId)
        {
            // Get the current user
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            // Find the cart item for the user and itemListingId
            var itemSale = user.ItemsInCart.Find(i => i.ItemListing.Id == itemListingId);

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
        public async Task<IActionResult> UpdateCart(Dictionary<long, int> quantities)
        {
            // Get the current user (assuming they are logged in)
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }

            foreach (var itemSale in user.ItemsInCart)
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
        // This route is deprecated and will redirect to checkoutreview
        public async Task<IActionResult> PrepareCheckout()
        {
            // Get the current user (assuming they are logged in)
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Forbid(); // Deny access if not logged in
            }
            return RedirectToAction("CheckoutReview");
        }

        [HttpPost]
        public async Task<IActionResult> ProcessCheckout(CartControllerCheckoutModel model)
        {
            // Get and verify current user
            ApplicationUser user = await _userManager.GetUserAsync(User);
            if (user == null) return Forbid();



            Order order = user.CreateOrderFromCart();

            if (order.ItemsSold.IsNullOrEmpty())
            {
                return RedirectToAction("ViewCart");
            }

            foreach (ItemSale item in order.ItemsSold)
            {
                if (item.Quantity > item.ItemListing.Quantity || item.ItemListing.Quantity <= 0)
                {
                    return Forbid();
                }
            }

            foreach (ItemSale item in order.ItemsSold)
            {
                item.ItemListing.Quantity -= item.Quantity;
            }

            if (model.SelectedAddressIndex == -1)
            {
                order.ShippingAddress = user.PrimaryAddress;
            }
            else
            {
                order.ShippingAddress = user.SecondaryAddresses[model.SelectedAddressIndex];
            }

            order.PaymentMethod = user.PaymentMethods[model.SelectedPaymentIndex];

            if (user.OrderHistory == null) user.OrderHistory = new List<Order>();


            user.OrderHistory.Add(order);

            await _dbContext.SaveChangesAsync();

            user.ItemsInCart = new List<ItemSale>();

            await _userManager.UpdateAsync(user);

           
            return RedirectToAction("OrderHistory", "Order");
        }

        // Review cart details when checking out
        public async Task<IActionResult> CheckoutReview()
        {
            ApplicationUser user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return Forbid();
            }


            Order order = user.CreateOrderFromCart();

            if (order.ItemsSold.IsNullOrEmpty())
            {
                return RedirectToAction("ViewCart");
            }

            CartControllerCheckoutModel model = new CartControllerCheckoutModel(user, order);


            return View(model);
        }


    }
}
