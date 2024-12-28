namespace MediaXAPIs.Data.Models
{
    public class EmailModel
    {
        public int EmailId { get; set; }
        public DateTime QueueDate { get; set; }
        public string EmailAddress { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public string EmailAttachment { get; set; }
        public DateTime? LastRetryDate { get; set; }
        public string RetryCount { get; set; }
        public string QueueGroup { get; set; }
        public string CheckSum { get; set; }
        public string CustomerReference { get; set; }
        public string Status { get; set; }
    }
}
