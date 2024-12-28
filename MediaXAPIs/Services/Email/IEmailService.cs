using MediaXAPIs.Data.Models;

namespace MediaXAPIs.Services.Email
{
    public interface IEmailService
    {
        Task<EmailResponse> SendEmail(EmailRequest request);
        Task SendEmailAsync(string to, string subject, string body);
        Task SignUpNotification(EmailRequest model);
    }
}
