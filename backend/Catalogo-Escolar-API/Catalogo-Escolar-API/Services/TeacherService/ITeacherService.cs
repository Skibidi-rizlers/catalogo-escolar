namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Interface for TeacherService
    /// </summary>
    public interface ITeacherService
    {
        /// <summary>
        /// Returns a teacher with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Teacher if it exists</returns>
        Task<Teacher?> Get(string email);
        /// <summary>
        /// Adds a new teacher to the database.
        /// </summary>
        /// <param name="data">User data of teacher</param>
        /// <returns>Result of operation</returns>
        Task<bool> Add(User data);
        /// <summary>
        /// Changes the password of the teacher with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="newPassword">New desired password</param>
        /// <returns>Result of operation</returns>
        Task<bool> ChangePassword(string email, string newPassword);

        /// <summary>
        /// Delete a course with the provided id.
        /// </summary>
        /// <param name="courseId">Id of course</param>
        /// <param name="teacherId">Id of teacher</param>
        /// <returns>Result of operation</returns>
        Task<bool> DeleteCourse(int courseId, int teacherId);
    }
}