using Catalogo_Escolar_API.Settings.AuthSettings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Catalogo_Escolar_API.Helpers
{
    /// <summary>
    /// Generates JWT tokens for authentication.
    /// </summary>
    public class JWTGenerator
    {
        private readonly IAuthSettings _authSettings;
        /// <summary>
        /// Constructor for JWTGenerator
        /// </summary>
        /// <param name="authSettings"></param>
        public JWTGenerator(IAuthSettings authSettings)
        {
            _authSettings = authSettings;
        }
        /// <summary>
        /// Generates a JWT token for a user.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>JWT</returns>
        public string GenerateToken(User user)
        {
            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.SecretKey);
            var credentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = GenerateClaims(user),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = credentials,
            };

            var token = handler.CreateToken(tokenDescriptor);
            return handler.WriteToken(token);
        }

        /// <summary>
        /// Generates claims for a student.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Claims for the JWT</returns>
        private static ClaimsIdentity GenerateClaims(User user)
        {
            string roleName = user.Email.EndsWith("@student.com") ? "student" : "teacher";
            var claims = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, roleName)
            ]);

            return claims;
        }
    }
}
