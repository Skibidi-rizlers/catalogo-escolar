﻿namespace Catalogo_Escolar_API.Model.DTO
{
    /// <summary>
    /// Represents the schema of the login data received on the Auth Login endpoint.
    /// </summary>
    public class LoginDTO
    {
        /// <summary>
        /// Email of user
        /// </summary>
        required public string Email { get; set; }
        /// <summary>
        /// Password of user
        /// </summary>
        required public string Password { get; set; }
    }
}
