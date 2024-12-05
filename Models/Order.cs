using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class Order
    {
        public long Id { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal TotalAmount { get; private set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal ShippingCost { get; set; }
        public DateTime PurchaseDate { get; set; }

        public virtual List<ItemSale> ItemsSold { get; set; }

        public virtual CardPaymentMethod PaymentMethod { get; set; }

        public virtual Address ShippingAddress { get; set; }

    }
}