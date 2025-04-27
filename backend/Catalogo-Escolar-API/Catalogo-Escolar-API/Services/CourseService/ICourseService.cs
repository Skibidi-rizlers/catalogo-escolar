namespace Catalogo_Escolar_API.Services.CourseService
{
    /// <summary>
    /// Interface for CourseService
    /// </summary>
    public interface ICourseService
    {
        /// <summary>
        /// Return a course with the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Course if it exists or null</returns>
        Task<Class?> Get(int id);
        /// <summary>
        /// Return a course with the provided ID.
        /// </summary>
        /// <param name="courseId"></param>
        /// <param name="teacherId"></param>
        /// <returns>Course if it exists or null</returns>
        Task<Class?> GetCourseForIdAndTeacher(int courseId, int teacherId);
        /// <summary>
        /// Adds a new course to the database.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>Result of operation</returns>
        Task<bool> AddCourse(Class course);
        /// <summary>
        /// Updates the course from the database.
        /// </summary>
        /// <param name="course"></param>
        /// <returns>Result of operation</returns>
        Task<bool> UpdateCourse(Class course);
        /// <summary>
        /// Deletes the course from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result of operation</returns>
        Task<bool> DeleteCourse(int id);
    }
}
