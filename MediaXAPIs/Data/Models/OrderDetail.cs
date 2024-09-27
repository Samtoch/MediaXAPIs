namespace MediaXAPIs.Data.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string OrderReference { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public bool DelFlag { get; set; } = false;
    }
}
