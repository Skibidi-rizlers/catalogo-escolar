namespace Catalogo_Escolar_API.Model.Payloads.Auth
{
    /// <summary>
    /// Represents the schema of the login data received on the Auth Login endpoint.
    /// </summary>
    public class LoginPayload
    {
        /// <summary>
        /// Email of user
        /// </summary>
        required public string Email { get; set; }
        /// <summary>
        /// Password of user
        /// </summary>
        required public string Password { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"LoginPayload (email: {Email}, password: {Password})";
        }
    }
}
