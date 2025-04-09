namespace Catalogo_Escolar_API.Model.DTO
{
    /// <summary>
    /// Represents the schema of the change password data received on the Auth ChangePassword endpoint.
    /// </summary>
    public class ChangePasswordDTO
    {
        /// <summary>
        /// Current password of user
        /// </summary>
        required public string OldPassword { get; set; }
        /// <summary>
        /// Desired password of user
        /// </summary>
        required public string NewPassword { get; set; }
    }
}
