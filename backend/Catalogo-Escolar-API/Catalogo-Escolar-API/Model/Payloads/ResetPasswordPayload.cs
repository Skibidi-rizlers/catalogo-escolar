namespace Catalogo_Escolar_API.Model.DTO
{
    /// <summary>
    /// Represents the schema of the reset password data received on the Auth ResetPassword endpoint.
    /// </summary>
    public class ResetPasswordPayload
    {
        /// <summary>
        /// Email
        /// </summary>
        required public string Email { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"ResetPasswordPayload (email: {Email})";
        }
    }
}
