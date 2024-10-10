namespace MediaXAPIs.Data.Models
{
    public class UserProduct
    {
        public int Id { get; set; } 
        public int UserId { get; set; } 

        public int ProductId { get; set; } 

        public decimal Discount { get; set; } = 0; 

        public DateTime ProductDate { get; set; } = DateTime.Now; 

        public bool DelFlag { get; set; } = false; 
    }

}
