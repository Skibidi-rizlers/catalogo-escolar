namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Interface for StudentService
    /// </summary>
    public interface IStudentService
    {
        /// <summary>
        /// Returns a student with the provided email and password.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Student if it exists</returns>
        Task<User?> GetStudent(string email, string password);
        /// <summary>
        /// Adds a new student to the database.
        /// </summary>
        /// <param name="student">Student</param>
        /// <returns>Result of operation</returns>
        Task<bool> AddStudent(User student);
        /// <summary>
        /// Changes the password of the student with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="newPassword">New desired password</param>
        /// <returns>Result of operation</returns>
        Task<bool> ChangePasswordForStudent(string email, string newPassword);
    }
}