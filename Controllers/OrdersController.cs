using Microsoft.AspNetCore.Mvc;
using CyberMall.Models;
using System;
using System.Collections.Generic;
using CyberMall.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CyberMall.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public OrderController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OrderHistory()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            var orders = currentUser.OrderHistory;
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Forbid();
            }

            var order = await _context.Set<Order>()
                .Include(o => o.ItemsSold)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if (order == null)
            {
                return NotFound();
            }
            // Remove order form users history and related itemsales
            currentUser.OrderHistory.Remove(order);

            if (order.ItemsSold != null)
            {
                foreach (ItemSale sale in order.ItemsSold)
                {
                    sale.ItemListing.Quantity += sale.Quantity;
                }
                _context.Set<ItemSale>().RemoveRange(order.ItemsSold);
            }

            // Remove the order
            _context.Set<Order>().Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(OrderHistory));
        }
    }
}