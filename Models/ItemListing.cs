using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class ItemListing
    {
        public virtual ApplicationUser Seller { get; set; }
        public long Id { get; set; } // Primary key
        public string ProductName { get; set; } // Name of the product
        public string Description { get; set; } // Description of the product
        public byte[] ImageData { get; set; } // Byte array to store image

        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Price { get; set; } // Price of the item

        public int Quantity { get; set; } // Available quantity

        [DefaultValue(true)]
        public bool Visible { get; set; }

        // Navigation property for related ItemSale
        public virtual ICollection<ItemSale> ItemSales { get; set; } // This allows you to access all ItemSale records related to this item
    }
}
