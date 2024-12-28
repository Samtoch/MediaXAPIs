namespace MediaXAPIs.Data.Models
{
    public class ProductDetailPlusImg : ProductDetail
    {
        public string? ImageUrl { get; set; }
        public int GlobalId { get; set; }
        public decimal? Discount { get; set; }
    }
}
