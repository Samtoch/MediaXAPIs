namespace MediaXAPIs.Data.Models
{
    public class ProductImage
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageId { get; set; }
        public string ImageString { get; set; }
        public bool IsMain { get; set; }
        public char DelFlag { get; set; }
    }

    public class ProductImageCreate
    {
        public int ProductId { get; set; }
        public string ImageId { get; set; }
        public string ImageString { get; set; }
        public string IsMain { get; set; }
    }

}
    