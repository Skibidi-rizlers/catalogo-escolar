namespace Catalogo_Escolar_API.Model.Payloads.Auth
{
    /// <summary>
    /// Represents the schema of the reset password data received on the Auth ResetPassword endpoint.
    /// </summary>
    public class ResetPasswordPayload
    {
        /// <summary>
        /// Email
        /// </summary>
        required public string EncodedId { get; set; }
        /// <summary>
        /// Password
        /// </summary>
        required public string Password { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"ResetPasswordPayload (encodedId: {EncodedId}, password: {Password})";
        }
        /// <summary>
        /// Checks if the payload is valid.
        /// </summary>
        /// <returns>Validity</returns>
        public bool IsValid()
        {
            if (Password.Length < 5)
            {
                return false;
            }

            return true;
        }
    }
}
