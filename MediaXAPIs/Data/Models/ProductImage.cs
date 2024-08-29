namespace MediaXAPIs.Data.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public string ProductId { get; set; }
        public string ImageId { get; set; }
        public string ImageString { get; set; }
        public string DelFlag { get; set; }
    }

    public class ProductImageCreate
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
        public string ImageString { get; set; }
    }

}
    