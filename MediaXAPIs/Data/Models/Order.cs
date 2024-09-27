namespace MediaXAPIs.Data.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public string Country { get; set; }
        public string AddressPlace { get; set; }
        public string AddressName { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OrderNote { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }
        public string PaymentType { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;
        public bool DelFlag { get; set; } = false;
        public string OrderReference { get; set; }

    }
}
