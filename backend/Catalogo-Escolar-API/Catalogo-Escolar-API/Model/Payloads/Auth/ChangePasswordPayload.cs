namespace Catalogo_Escolar_API.Model.Payloads.Auth
{
    /// <summary>
    /// Represents the schema of the change password data received on the Auth ChangePassword endpoint.
    /// </summary>
    public class ChangePasswordPayload
    {
        /// <summary>
        /// Current password of user
        /// </summary>
        required public string OldPassword { get; set; }
        /// <summary>
        /// Desired password of user
        /// </summary>
        required public string NewPassword { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"ChangePasswordPayLoad (oldPassword: {OldPassword}, password: {NewPassword})";
        }
    }
}
