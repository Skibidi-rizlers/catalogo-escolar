using Catalogo_Escolar_API.Model.DTOs;

namespace Catalogo_Escolar_API.Services.GradeService
{
    /// <summary>
    /// Interface for GradeService
    /// </summary>
    public interface IGradeService
    {
        /// <summary>
        /// Adds a new grade to the database.
        /// </summary>
        /// <param name="grade"></param>
        /// <returns>Result of operation</returns>
        Task<bool> Add(GradeDTO grade);
        /// <summary>
        /// Adds an array of grades to the database.
        /// </summary>
        /// <param name="grades"></param>
        /// <returns>Result of operation</returns>
        Task<bool> AddGrades(List<GradeDTO> grades);

        /// <summary>
        /// Returns the grade with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Grade or null</returns>
        Task<GradeDTO?> GetById(int id);
        /// <summary>
        /// Returns the grade for the provided user id and assignment id.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="assignmentId"></param>
        /// <returns>Grade or null</returns>
        Task<GradeDTO?> GetGradeForUser(int userId, int assignmentId);

        /// <summary>
        /// Returns the grades for the provided assignment id.
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns>List of grades</returns>
        Task<List<GradeWithStudentNameDTO>> GetGradesForAssignment(int assignmentId);
        /// <summary>
        /// Updates grade.
        /// </summary>
        /// <param name="grade"></param>
        /// <returns>Result of operation</returns>
        Task<bool> Update(GradeDTO grade);
        /// <summary>
        /// Deletes the grade with the provided id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result of operation</returns>
        Task<bool> Delete(int id);
    }
}
