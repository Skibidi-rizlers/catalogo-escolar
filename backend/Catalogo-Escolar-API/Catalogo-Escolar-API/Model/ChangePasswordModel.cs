﻿namespace Catalogo_Escolar_API.Model
{
    /// <summary>
    /// Represents the schema of the change password data received on the Auth ChangePassword endpoint.
    /// </summary>
    public class ChangePasswordModel
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
