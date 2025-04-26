using Catalogo_Escolar_API.Model.Payloads.Auth;

namespace Catalogo_Escolar_API.Helpers
{
    /// <summary>
    /// Represents the centralized logic of email generation.
    /// </summary>
    public class EmailFormatter
    {
        /// <summary>
        /// Generates a unique email
        /// </summary>
        /// <param name="registerDTO">User data</param>
        /// <param name="isEmailTaken">Function that check whether the email already exists</param>
        /// <returns>Unique email</returns>
        public static string GenerateUniqueEmail(RegisterPayload registerDTO, Func<string, bool> isEmailTaken)
        {
            string first = registerDTO.FirstName.ToLower();
            string last = registerDTO.LastName.ToLower();
            string role = registerDTO.RoleName.ToLower();

            string email = $"{first}.{last}@{role}.com";
            int counter = 1;

            while (isEmailTaken(email))
            {
                email = $"{first}.{last}{counter}@{role}.com";
                counter++;
            }

            return email;
        }

    }
}
