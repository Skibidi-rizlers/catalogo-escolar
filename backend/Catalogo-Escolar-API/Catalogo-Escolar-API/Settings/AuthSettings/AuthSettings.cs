namespace Catalogo_Escolar_API.Settings.AuthSettings
{
    /// <summary>
    /// Represents the settings for authentication, including the secret key used for signing JWT tokens.
    /// </summary>
    public class AuthSettings : IAuthSettings
    {
        /// <summary>
        /// Secret key used to sign the JWT token.
        /// </summary>
        public required string SecretKey { get; set; }
    }
}
