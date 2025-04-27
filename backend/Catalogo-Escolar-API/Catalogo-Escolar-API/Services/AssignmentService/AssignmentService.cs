using Catalogo_Escolar_API.Model;
using Catalogo_Escolar_API.Services.AuthService;

namespace Catalogo_Escolar_API.Services.AssignmentService
{
    /// <summary>
    /// Represents the Assignment service class
    /// </summary>
    public class AssignmentService : IAssignmentService
    {
        private readonly SchoolContext _context;
        private readonly ILogger<AssignmentService> _logger;
        /// <summary>
        /// Constructor of Assignment service class
        /// </summary>
        /// <param name="context">Database context</param>
        public AssignmentService(SchoolContext context, ILogger<AssignmentService> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc/>
        public Task<bool> AddAssignment(Assignment assignment)
        {
            try
            {
                _context.Assignments.Add(assignment);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Exception? loggedException = ex;
                while(loggedException.InnerException != null)
                {
                    loggedException = loggedException.InnerException;
                }
                _logger.LogError(loggedException.Message);
                return Task.FromResult(false);
            }
        }

        /// <inheritdoc/>
        public Task<bool> DeleteAssignment(int id)
        {
            try
            {
                Assignment? assignment = _context.Assignments.Find(id);
                if (assignment != null)
                {
                    _context.Assignments.Remove(assignment);
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Exception? loggedException = ex;
                while (loggedException.InnerException != null)
                {
                    loggedException = loggedException.InnerException;
                }
                _logger.LogError(loggedException.Message);
                return Task.FromResult(false);
            }
        }

        /// <inheritdoc/>
        public Task<Assignment?> GetAssignmentById(int id)
        {
            Assignment? assignment = _context.Assignments.Find(id);
            return Task.FromResult(assignment);
        }

        /// <inheritdoc/>
        public Task<List<Assignment>> GetAssignmentsByClassId(int classId)
        {
            var assignments = _context.Assignments
                .Where(a => a.ClassId == classId)
                .ToList();
            var list = new List<Assignment>();
            list.AddRange(assignments);
            return Task.FromResult(list);
        }

        /// <inheritdoc/>
        public Task<bool> UpdateAssignment(Assignment assignment)
        {
            try
            {
                Assignment? existingAssignment = _context.Assignments.Find(assignment.Id);
                if (existingAssignment != null)
                {
                    existingAssignment.Title = assignment.Title;
                    existingAssignment.Description = assignment.Description;
                    existingAssignment.DueDate = assignment.DueDate;
                    _context.SaveChanges();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Exception? loggedException = ex;
                while (loggedException.InnerException != null)
                {
                    loggedException = loggedException.InnerException;
                }
                _logger.LogError(loggedException.Message);
                return Task.FromResult(false);
            }
        }
    }
}
