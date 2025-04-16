namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Interface for StudentService
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Returns a student with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Student if it exists</returns>
        Task<Student?> Get(string email);
        /// <summary>
        /// Adds a new student to the database.
        /// </summary>
        /// <param name="data">User data of student</param>
        /// <returns>Result of operation</returns>
        Task<bool> Add(User data);
        /// <summary>
        /// Changes the password of the student with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="newPassword">New desired password</param>
        /// <returns>Result of operation</returns>
        Task<bool> ChangePassword(string email, string newPassword);
    }
}