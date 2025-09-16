using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace YuzTanimaTakip.Services
{
    public class EmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Make the method async
        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            var smtpSettings = _configuration.GetSection("EmailSettings");

            try
            {
                using (var smtpClient = new SmtpClient(smtpSettings["Host"], int.Parse(smtpSettings["Port"]!)))
                {
                    smtpClient.Credentials = new NetworkCredential(smtpSettings["Username"], smtpSettings["Password"]);
                    smtpClient.EnableSsl = smtpSettings.GetValue<bool>("EnableSsl");

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(smtpSettings["From"]!),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(toEmail);

                    // Use async method to send email
                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle the exception appropriately
                throw new Exception("An error occurred while sending the email.", ex);
            }
        }
    }
}
