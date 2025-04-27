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

        /// <summary>
        /// Adds a course to the teacher with the provided id.
        /// </summary>
        /// <param name="courseName">Course name</param>
        /// <param name="teacherId">Id of teacher</param>
        /// <returns>Result of operation</returns>
        Task<bool> AddCourse(int teacherId, string courseName);

        /// <summary>
        /// Modifies a course with the provided old name.
        /// </summary>
        /// <param name="teacherId">Id of teacher</param>
        /// <param name="courseName">New course name</param>
        /// <param name="oldCourseName">Old course name, used for identification</param>
        /// <returns>Result of operation</returns>
        Task<bool> ModifyCourse(string oldCourseName, int teacherId, string courseName);

        /// <summary>
        /// Adds a student to a course with the provided id.
        /// </summary>
        /// <param name="studentId">Id of student </param>
        /// <param name="courseId">Id of course</param>
        /// <returns>Result of operation</returns>
        Task<bool> AddStudentToCourse(int studentId, int courseId);
        /// <summary>
        /// Deletes a student from a course with the provided id.
        /// <param name="studentId">Id of student </param>
        /// <param name="courseId">Id of course</param>
        /// <returns>Result of operation</returns>
        Task<bool> DeleteStudentFromCourse(int studentId, int courseId);


    }
}