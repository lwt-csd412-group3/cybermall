using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CyberMall.Models
{
    public class Order
    {
        public long Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }

        public DateTime PurchaseDate { get; set; }

        public virtual List<ItemSale> ItemsSold { get; set; }

        public virtual CardPaymentMethod PaymentMethod { get; set; }

        public virtual Address ShippingAddress { get; set; }

        public void CalculateTotal()
        {
            decimal subtotal = ItemsSold?.Sum(item => item.TotalPrice) ?? 0;
            TotalAmount = subtotal + TaxAmount + ShippingCost;
        }
    }
}