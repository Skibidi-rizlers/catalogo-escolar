namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Interface used for email functionality
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Sends an email to the provided email address with a reset link.
        /// </summary>
        /// <param name="toEmail"></param>
        /// <param name="resetLink"></param>
        /// <returns>Result of operation (if the email was sent succesfully)</returns>
        bool SendEmail(string toEmail, string resetLink);
    }
}
