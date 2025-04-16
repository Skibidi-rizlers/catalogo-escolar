namespace Catalogo_Escolar_API.Services.UniqueService
{
    /// <summary>
    /// Represents the service that check if the email already exists in database
    /// </summary>
    public interface IUniqueService
    {
        /// <summary>
        /// Checks whether the email exists in the database
        /// </summary>
        /// <param name="email"></param>
        /// <returns>Result of operation</returns>
        bool EmailExists(string email);
    }
}
