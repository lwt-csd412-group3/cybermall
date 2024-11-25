using System;

namespace CyberMall.Models
{
    public class PreviousOrders
    {
        public double TotalAmount { get; private set; }
        public double TaxAmount { get; set; }
        public double ShippingCost { get; set; }
        public DateTime PurchaseDate { get; set; }
    }
}