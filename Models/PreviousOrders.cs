using System;

namespace CyberMall.Models
{
    public class PreviousOrders
    {
        public decimal TotalAmount { get; private set; }
        public decimal TaxAmount { get; set; }
        public decimal ShippingCost { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}