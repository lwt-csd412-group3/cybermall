namespace CyberMall.Models
{
    public class Order
    {
        public int Id { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }
    }
}
