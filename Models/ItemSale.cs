﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class ItemSale
    {

        public int Id { get; set; } // Primary key for ItemSale
        public int ItemListingId { get; set; } // Foreign key to ItemListing
        public virtual ItemListing ItemListing { get; set; } // Navigation property to ItemListing

        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Price { get; set; } // Price of the item at the time of purchase
        public int Quantity { get; set; } // Quantity the user wants to buy
        
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Discount { get; set; } // Discount applied to the item

        // add a reference to the User who added the item to the cart to manage the order history
        public string UserId { get; set; } // Foreign key for ApplicationUser (user who added to cart)
        public virtual ApplicationUser User { get; set; } // Navigation property to ApplicationUser
    }
}
