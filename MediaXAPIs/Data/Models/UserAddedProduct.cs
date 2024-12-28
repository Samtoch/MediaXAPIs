namespace MediaXAPIs.Data.Models
{
    public class UserAddedProduct
    {
        public int Id { get; set; }
        public int GlobalId { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public int? ProductId { get; set; } // Nullable to handle LEFT JOIN
        public decimal? Discount { get; set; } // Nullable to handle LEFT JOIN
    }
}
