using Catalogo_Escolar_API.Model.DTO;

namespace Catalogo_Escolar_API.Helpers
{
    /// <summary>
    /// Represents the centralized logic of email generation.
    /// </summary>
    public class EmailFormatter
    {
        /// <summary>
        /// Generates a new standard email for a registerDTO
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns></returns>
        public static string GetEmail(RegisterDTO registerDTO)
        {
            string email = registerDTO.FirstName.ToLower() + "." + registerDTO.LastName.ToLower() + "@" + registerDTO.RoleName + ".com";
            return email;
        }
    }
}
