namespace Catalogo_Escolar_API.Settings.AuthSettings
{
    /// <summary>
    /// Implementation of IAuthSettings.
    /// </summary>
    public class AuthSettings : IAuthSettings
    {
        /// <inheritdoc/>
        public required string SecretKey { get; set; }
    }
}
