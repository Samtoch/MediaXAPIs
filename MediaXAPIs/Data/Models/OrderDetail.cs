using System.ComponentModel.DataAnnotations;

namespace MediaXAPIs.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(1, double.MaxValue, ErrorMessage = "Price must be at atleast 1.")]
        public int Quantity { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        [Required]
        public string OrderReference { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public bool DelFlag { get; set; } = false;
    }
}
