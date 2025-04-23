using Catalogo_Escolar_API.Services.StudentService;
using System.Net.Mail;
using System.Net;
using Catalogo_Escolar_API.Settings.AuthSettings;

namespace Catalogo_Escolar_API.Services.EmailService
{
    /// <summary>
    /// Implementation of IEmailService
    /// </summary>
    public class EmailService : IEmailService
    {
        private IEmailSettings _emailSettings;
        /// <summary>
        /// Constructor for EmailService
        /// </summary>
        /// <param name="emailSettings"></param>
        public EmailService(IEmailSettings emailSettings)
        {
            _emailSettings = emailSettings;
        }

        /// <inheritdoc/>
        public bool SendEmail(string toEmail, string resetLink)
        {
            try
            {
                MailAddress? fromAddress = new MailAddress(_emailSettings.FromEmail, "Catalogo Escolar");
                MailAddress? toAddress = new MailAddress(toEmail);

                const string user = "apikey";

                var smtp = new SmtpClient
                {
                    Host = "smtp.sendgrid.net",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(user, _emailSettings.ApiKey)
                };

                const string subject = "Reset Your Password";
                string body = $"Click the link below to reset your password:\n{resetLink}";

                using var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                };

                smtp.Send(message);
                Console.WriteLine("Email sent successfully!");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to send email: " + ex.Message);
                return false;
            }
        }
    }
}
