namespace Catalogo_Escolar_API.Services.UniqueService
{
    /// <summary>
    /// Represents the service that represents the functionality on the user
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Checks whether the email exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Result of operation</returns>
        bool EmailExists(string email);

        /// <summary>
        /// Gets the user model with the provided id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User or null</returns>
        User? GetUserById(int id);

        /// <summary>
        /// Updates the user
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Result of operation</returns>
        bool Update(User user);
    }
}
