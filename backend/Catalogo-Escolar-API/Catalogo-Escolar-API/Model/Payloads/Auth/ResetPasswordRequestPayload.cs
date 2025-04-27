namespace Catalogo_Escolar_API.Model.Payloads.Auth
{
    /// <summary>
    /// Represents the schema of the reset password request data received on the Auth ResetPasswordRequest endpoint.
    /// </summary>
    public class ResetPasswordRequestPayload
    {
        /// <summary>
        /// Email
        /// </summary>
        required public string Email { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"ResetPasswordRequestPayload (email: {Email})";
        }
    }
}
