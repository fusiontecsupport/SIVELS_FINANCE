using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ERP.API.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("SmtpSettings");
            var host = smtpSettings["Host"];
            var port = int.Parse(smtpSettings["Port"] ?? "587");
            var username = smtpSettings["Username"];
            var password = smtpSettings["Password"];
            var fromEmail = smtpSettings["FromEmail"];

            try
            {
                using (var client = new SmtpClient(host, port))
                {
                    client.Credentials = new NetworkCredential(username, password);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(fromEmail),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = false
                    };
                    mailMessage.To.Add(toEmail);

                    await client.SendMailAsync(mailMessage);
                }
            }
            catch (System.Exception ex)
            {
                // Print to console so we can see the OTP during local testing even if SMTP fails
                System.Console.WriteLine("========== EMAIL SEND FAILED (Likely due to dummy SMTP settings) ==========");
                System.Console.WriteLine($"To: {toEmail}");
                System.Console.WriteLine($"Subject: {subject}");
                System.Console.WriteLine($"Body:\n{body}");
                System.Console.WriteLine($"Error: {ex.Message}");
                System.Console.WriteLine("===========================================================================");
                // We don't throw the exception here so that the OTP flow can still be tested locally.
            }
        }
    }
}
