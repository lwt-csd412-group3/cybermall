using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace CyberMall.Models
{
    public class Order
    {

        // using a constant for demonstation for tax and shipping

        public const decimal DefaultTaxRate = 0.10m;
        public const decimal DefaultShippingCost = 10.00m;
        


        public long Id { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal SubtotalAmount
        {
            get
            {
                return ItemsSold.Sum(i => i.TotalPrice);
            }
        }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingCost { get; set; }

        public DateTime PurchaseDate { get; set; }

        public virtual List<ItemSale> ItemsSold { get; set; }

        public virtual CardPaymentMethod PaymentMethod { get; set; }

        public virtual Address ShippingAddress { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount
        {
            get
            {
                return (SubtotalAmount + ShippingCost + TaxAmount);
            }
        }
    }
}