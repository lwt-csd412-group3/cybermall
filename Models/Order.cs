using System.ComponentModel.DataAnnotations.Schema;

namespace CyberMall.Models
{
    public class Order
    {
        public int Id { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")] // Define precision and scale for decimal
        public decimal Discount { get; set; }
    }
}
