using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class ItemListing
    {
        public ApplicationUser Seller { get; set; }
        public string SellerId { get; set; } // Foreign key for ApplicationUser
        public int ItemListingId { get; set; } // Primary key
        public string ProductName { get; set; } // Name of the product
        public string Description { get; set; } // Description of the product
        public byte[] ImageData { get; set; } // Byte array to store image

        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Price { get; set; } // Price of the item

        public int Quantity { get; set; } // Available quantity

        // Navigation property for related ItemSale
        public ICollection<ItemSale> ItemSales { get; set; } // This allows you to access all ItemSale records related to this item
    }
}
