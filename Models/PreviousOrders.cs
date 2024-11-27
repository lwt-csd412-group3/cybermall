using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class PreviousOrders
    {
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal TotalAmount { get; private set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal ShippingCost { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}