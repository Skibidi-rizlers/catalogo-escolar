namespace Catalogo_Escolar_API.Settings.AuthSettings
{
    /// <summary>
    /// Implementation of IEmailSettings
    /// </summary>
    public class EmailSettings : IEmailSettings
    {
        /// <inheritdoc/>
        public required string FromEmail { get; set; }
        /// <inheritdoc/>
        public required string ApiKey { get; set; }
    }
}
