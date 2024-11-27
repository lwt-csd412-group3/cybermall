using Microsoft.AspNetCore.Mvc;
using CyberMall.Models;
using System;
using System.Collections.Generic;

namespace CyberMall.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult OrderHistory()
        {
            var orders = new List<PreviousOrders>
            {
                // placeholder orders
                new PreviousOrders
                {
                    TaxAmount = 2.50M,
                    ShippingCost = 5.99M,
                    PurchaseDate = DateTime.Now.AddDays(-5)
                },
                new PreviousOrders
                {
                    TaxAmount = 1.75M,
                    ShippingCost = 5.99M,
                    PurchaseDate = DateTime.Now.AddDays(-10)
                }
            };

            return View(orders);
        }
    }
}