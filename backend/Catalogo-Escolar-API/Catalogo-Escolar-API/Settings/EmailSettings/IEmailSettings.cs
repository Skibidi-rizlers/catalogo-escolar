namespace Catalogo_Escolar_API.Settings.AuthSettings
{
    /// <summary>
    /// Interface for EmailSettings
    /// </summary>
    public interface IEmailSettings
    {
        /// <summary>
        /// Transport sender email
        /// </summary>
        public string FromEmail { get; set; }
        /// <summary>
        /// Transport password
        /// </summary>
        public string ApiKey { get; set; }
    }
}
