using Catalogo_Escolar_API.Model.DTO;

namespace Catalogo_Escolar_API.Services.AuthService
{
    /// <summary>
    /// Interface for AuthService
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Returns a JWT token if the user is authenticated.
        /// </summary>
        /// <param name="loginDTO">Login data of user</param>
        /// <returns>JWT</returns>
        Task<string?> Login(LoginDTO loginDTO);

        /// <summary>
        /// Changes the password of the user.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="oldPassword">Old password of user</param>
        /// <param name="newPassword">New desire password of user</param>
        /// <returns>Result of operation</returns>
        Task<bool> ChangePassword(string email, string oldPassword, string newPassword);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="registerDTO"></param>
        /// <returns>Result of operation</returns>
        Task<bool> Register(RegisterDTO registerDTO);
    }
}
