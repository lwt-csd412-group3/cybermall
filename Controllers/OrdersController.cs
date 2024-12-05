using Microsoft.AspNetCore.Mvc;
using CyberMall.Models;
using System;
using System.Collections.Generic;
using CyberMall.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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
    }
}