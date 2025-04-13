namespace Catalogo_Escolar_API.Services.UniqueService
{
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
