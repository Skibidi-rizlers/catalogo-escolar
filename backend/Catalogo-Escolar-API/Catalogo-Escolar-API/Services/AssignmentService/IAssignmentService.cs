using Catalogo_Escolar_API.Model;

namespace Catalogo_Escolar_API.Services.AuthService
{
    /// <summary>
    /// Interface for AssignmentService
    /// </summary>
    public interface IAssignmentService
    {
        /// <summary>
        /// Returns a list of assignments for the provided class ID.
        /// </summary>
        /// <param name="classId"></param>
        /// <returns>List of assignments</returns>
        Task<List<Assignment>> GetAssignmentsByClassId(int classId);
        /// <summary>
        /// Return an assignment with the provided ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Assignment</returns>
        Task<Assignment?> GetAssignmentById(int id);
        /// <summary>
        /// Adds a new assignment to the database.
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns>Result of operation</returns>
        Task<bool> AddAssignment(Assignment assignment);
        /// <summary>
        /// Updates the assignment from the database.
        /// </summary>
        /// <param name="assignment"></param>
        /// <returns>Result of operation</returns>
        Task<bool> UpdateAssignment(Assignment assignment);
        /// <summary>
        /// Deletes the assignment from the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Result of operation</returns>
        Task<bool> DeleteAssignment(int id);

        /// <summary>
        /// Grades an assignment for a student.
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <param name="studentId"></param>
        /// <param name="grade"></param>
        /// <returns>Result of operation</returns>
        Task<bool> GradeAssignment(int assignmentId, int studentId, double grade);
    }
}
