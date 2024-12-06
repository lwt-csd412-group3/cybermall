using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CyberMall.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public virtual Address PrimaryAddress { get; set; }

        public virtual List<Address> SecondaryAddresses { get; set; }

        public virtual List<Order> OrderHistory { get; set; }


        public virtual List<ItemSale> ItemsInCart { get; set; }

        public virtual List<CardPaymentMethod> PaymentMethods { get; set; }

        public Order CreateOrderFromCart()
        {
            // Create order from cart items
            var order = new Order
            {
                PurchaseDate = DateTime.UtcNow,
                ItemsSold = ItemsInCart.Select(item => new ItemSale
                {
                    ItemListing = item.ItemListing,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Discount = item.Discount,
                    User = this,
                }).ToList(),
                ShippingAddress = PrimaryAddress
            };

            order.TaxAmount = order.SubtotalAmount * Order.DefaultTaxRate;
            order.ShippingCost = Order.DefaultShippingCost;

            return order;
        }
    }
}
