namespace Catalogo_Escolar_API.Services.AuthService
{
    /// <summary>
    /// Represents the authentication service. It is used to authenticate users and any actions it may include, such as JWT creation.
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Returns a JWT token if the user is authenticated.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>JWT</returns>
        Task<string?> Login(string email, string password);

        /// <summary>
        /// Changes the password of the user.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="oldPassword">Old password of user</param>
        /// <param name="newPassword">New desire password of user</param>
        /// <returns>Success / Failure of operation</returns>
        Task<bool> ChangePassword(string email, string oldPassword, string newPassword);
    }
}
