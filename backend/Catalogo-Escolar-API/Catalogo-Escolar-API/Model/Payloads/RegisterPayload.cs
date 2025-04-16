using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Catalogo_Escolar_API.Model.DTO
{
    /// <summary>
    /// Represents the schema of the register data received on the Auth Register endpoint.
    /// </summary>
    public class RegisterPayload
    {
        /// <summary>
        /// First name of new user.
        /// </summary>
        required public string FirstName { get; set; }
        /// <summary>
        /// Last name of new user.
        /// </summary>
        required public string LastName { get; set; }
        /// <summary>
        /// Name of role of new user.
        /// </summary>
        required public string RoleName { get; set; }
        /// <summary>
        /// Password of new user
        /// </summary>
        required public string Password { get; set; }

        /// <inheritdoc/>
        public override string? ToString()
        {
            return $"RegisterPayload (firstname: {FirstName}, lastname: {LastName}, role: {RoleName}, password: {Password})";
        }
    }
}
