using System.Net.Mail;
using System.Net;
using MediaXAPIs.Data.Models;
using NLog;
using Humanizer;
using MediaXAPIs.Utilities;

namespace MediaXAPIs.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private static readonly Logger log = LogManager.GetCurrentClassLogger();

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var mailSettings = _configuration.GetSection("MailSettings");
            string host = mailSettings["MailHost"];
            int port = int.Parse(mailSettings["MailPort"]);
            string username = mailSettings["MailUsername"];
            string password = mailSettings["Password"];
            bool enableSsl = bool.Parse(mailSettings["EnableSSL"]);
            bool credentialRequired = bool.Parse(mailSettings["CredentialRequired"]);
            string mailFrom = mailSettings["MailFrom"];

            var mailMessage = new MailMessage
            {
                From = new MailAddress(mailFrom),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(to);

            using (var smtpClient = new SmtpClient(host, port))
            {
                smtpClient.EnableSsl = enableSsl;

                if (credentialRequired)
                {
                    smtpClient.Credentials = new NetworkCredential(username, password);
                }

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
            }
        }

        public async Task SignUpNotification(EmailRequest model)
        {
            try
            {
                string projectRoot = Directory.GetCurrentDirectory();
                string logoPath = Path.Combine(projectRoot + "/Utilities", "logo.png");
                byte[] logoBytes = System.IO.File.ReadAllBytes(logoPath);
                string logoBase64 = Convert.ToBase64String(logoBytes);

                string base64Image = ConvertImageToBase64(logoPath);

                if (!string.IsNullOrEmpty(model.ToEmail))
                {
                    string emailMessage = "";

                    string htmlFilePath = Path.Combine(projectRoot, "Utilities/SignUpNotification.html");

                    if (System.IO.File.Exists(htmlFilePath))
                    {
                        FileStream f1 = new FileStream(htmlFilePath, FileMode.Open);
                        StreamReader sr = new StreamReader(f1);
                        emailMessage = emailMessage + sr.ReadToEnd();
                        emailMessage = emailMessage.Replace("[FirstName]", model.Firstname);

                        // Replace the img tag with Base64 encoded image
                        string updatedHtmlContent = ReplaceImgTagWithBase64(emailMessage, "cid:MediaXLogo", base64Image);

                        //emailMessage = emailMessage.Replace("[LogoBase64]", logoBase64);  // Inject logo

                        f1.Close();
                    }

                    var email = new EmailRequest();
                    email.isBodyHtml = true;
                    email.Message = emailMessage;
                    email.ToEmail = model.ToEmail;
                    email.Subject = "WELCOME TO MEDIAX " + DateTime.Now;
                    SendEmail(email);
                }
            }
            catch (Exception ex)
            {
                var errorLog = "Error" + ex.Message + " ; Date: " + DateTime.Now + Environment.NewLine;
                log.Error(errorLog);
            }
        }

        public async Task<EmailResponse> SendEmail(EmailRequest request)
        {
            EmailResponse response = new EmailResponse();
            EmailModel model = new EmailModel();

            var mailSettings = _configuration.GetSection("MailSettings");
            string host = mailSettings["MailHost"];
            int port = int.Parse(mailSettings["MailPort"]);
            string username = mailSettings["MailUsername"];
            string password = mailSettings["Password"];
            bool enableSsl = bool.Parse(mailSettings["EnableSSL"]);
            bool credentialRequired = bool.Parse(mailSettings["CredentialRequired"]);
            string mailFrom = mailSettings["MailFrom"];

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(mailFrom); //OR new MailAddress(fromEmailId, accountName);
            mailMessage.Subject = request.Subject;
            mailMessage.Body = request.Message;
            mailMessage.IsBodyHtml = true;
            mailMessage.To.Add(request.ToEmail);
            if (!string.IsNullOrEmpty(request.ToCC))
            {
                mailMessage.CC.Add(request.ToCC);
            }

            using (var smtpClient = new SmtpClient(host, port))
            {
                //smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtpClient.EnableSsl = enableSsl;

                if (credentialRequired)
                {
                    smtpClient.Credentials = new NetworkCredential(username, password);
                }

                try
                {
                    await smtpClient.SendMailAsync(mailMessage);
                    response.Status = true;
                    response.ResponseString = "Sent";
                    Console.WriteLine("Email sent successfully.");
                }
                catch (Exception ex)
                {
                    response.Status = false;
                    response.ResponseString = "Not Sent";
                    Console.WriteLine($"Failed to send email: {ex.Message}");
                }
                response.Recipient = request.ToEmail;
                return response;
            }

        }

        public static string ConvertImageToBase64(string imagePath)
        {
            byte[] imageBytes = File.ReadAllBytes(imagePath);
            string base64String = Convert.ToBase64String(imageBytes);

            // Determine MIME type based on file extension
            string mimeType = Path.GetExtension(imagePath).ToLower() switch
            {
                ".png" => "image/png",
                ".jpg" => "image/jpeg",
                ".jpeg" => "image/jpeg",
                ".gif" => "image/gif",
                _ => throw new InvalidOperationException("Unsupported image format")
            };

            return $"data:{mimeType};base64,{base64String}";
        }

        public static string ReplaceImgTagWithBase64(string htmlContent, string cid, string base64Image)
        {
            // Find and replace the img tag
            string imgTag = $"<img src='{cid}'";
            return htmlContent.Replace(imgTag, $"<img src='{base64Image}'");
        }
    }

    
}
