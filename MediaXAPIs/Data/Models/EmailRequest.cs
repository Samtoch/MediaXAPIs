namespace MediaXAPIs.Data.Models
{
    public class EmailRequest
    {
        public bool isBodyHtml {get;  set; }
        public string Message { get; set; }
        public string Subject { get; set; }
        public string ToEmail { get; set; }
        public string Firstname { get; set; }
        public string ToCC { get; set; }
    }
}
