namespace Catalogo_Escolar_API.Settings.AuthSettings
{
    /// <summary>
    /// Interface for AuthSettings
    /// </summary>
    public interface IAuthSettings
    {
        /// <summary>
        /// Secret key used to sign the JWT token.
        /// </summary>
        string SecretKey { get; set; }
    }
}
