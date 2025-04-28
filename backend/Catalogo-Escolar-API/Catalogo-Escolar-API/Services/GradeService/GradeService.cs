using Catalogo_Escolar_API.Model.DTOs;

namespace Catalogo_Escolar_API.Services.GradeService
{
    /// <summary>
    /// Service for managing grades
    /// </summary>
    public class GradeService : IGradeService
    {
        private readonly SchoolContext _context;
        private readonly ILogger<GradeService> _logger;
        /// <summary>
        /// Constructor of the service
        /// </summary>
        /// <param name="context">Database context</param>
        /// <param name="logger">Logger</param>
        public GradeService(SchoolContext context, ILogger<GradeService> logger)
        {
            _context = context;
            _logger = logger;
        }

        ///<inheritdoc/>
        public Task<bool> Add(GradeDTO grade)
        {
            try
            {
                var newGrade = new Grade
                {
                    StudentId = grade.StudentId,
                    AssignmentId = grade.AssignmentId,
                    Value = grade.Value,
                    GivenAt = grade.GivenAt
                };
                _context.Grades.Add(newGrade);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding grade");
                return Task.FromResult(false);
            }
        }

        ///<inheritdoc/>
        public Task<bool> AddGrades(List<GradeDTO> grades)
        {
            try
            {
                foreach (var grade in grades)
                {
                    var newGrade = new Grade
                    {
                        StudentId = grade.StudentId,
                        AssignmentId = grade.AssignmentId,
                        Value = grade.Value,
                        GivenAt = grade.GivenAt
                    };
                    _context.Grades.Add(newGrade);
                }
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding grades");
                return Task.FromResult(false);
            }
        }

        ///<inheritdoc/>
        public Task<bool> Delete(int id)
        {
            try
            {
                var grade = _context.Grades.FirstOrDefault(g => g.Id == id);
                if (grade == null)
                {
                    _logger.LogWarning("Grade with id {Id} not found", id);
                    return Task.FromResult(false);
                }
                _ = _context.Grades.Remove(grade);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting grade");
                return Task.FromResult(false);
            }
        }

        ///<inheritdoc/>
        public Task<GradeDTO?> GetById(int id)
        {
            try
            {
                var grade = _context.Grades.FirstOrDefault(g => g.Id == id);
                return Task.FromResult<GradeDTO?>(grade != null ? new GradeDTO
                {
                    Id = grade.Id,
                    StudentId = grade.StudentId,
                    AssignmentId = grade.AssignmentId,
                    Value = grade.Value,
                    GivenAt = grade.GivenAt
                } : null);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error getting grade by id");
                return Task.FromResult<GradeDTO?>(null);
            }
        }

        ///<inheritdoc/>
        public Task<List<GradeDTO>> GetGradesForAssignment(int assignmentId)
        {
            try
            {
                var list = _context.Grades.Where(grade => grade.AssignmentId == assignmentId).ToList();
                var resultList = new List<GradeDTO>();
                foreach (var item in list) 
                {
                    var gradeDTO = new GradeDTO
                    {
                        Id = item.Id,
                        StudentId = item.StudentId,
                        AssignmentId = item.AssignmentId,
                        Value = item.Value,
                        GivenAt = item.GivenAt
                    };
                    resultList.Add(gradeDTO);
                }
                return Task.FromResult(resultList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting grades for assignment");
                return Task.FromResult(new List<GradeDTO>());
            }
        }

        ///<inheritdoc/>
        public Task<bool> Update(GradeDTO grade)
        {
            try
            {
                var existingGrade = _context.Grades.FirstOrDefault(g => g.Id == grade.Id);
                if (existingGrade == null)
                {
                    _logger.LogWarning("Grade with id {Id} not found", grade.Id);
                    return Task.FromResult(false);
                }
                existingGrade.Value = grade.Value;
                existingGrade.GivenAt= grade.GivenAt;
                _context.Update(existingGrade);
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating grade");
                return Task.FromResult(false);
            }
        }
    }
}
