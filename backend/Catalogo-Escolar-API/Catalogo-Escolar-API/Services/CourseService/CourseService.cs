
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.CourseService
{
    /// <summary>
    /// Implementation of course service
    /// </summary>
    public class CourseService : ICourseService
    {
        private readonly SchoolContext _context;
        private readonly ILogger<CourseService> _logger;
        /// <summary>
        /// Constructor of course service class
        /// </summary>
        /// <param name="context"></param>
        /// <param name="logger"></param>
        public CourseService(SchoolContext context, ILogger<CourseService> logger)
        {
            _context = context;
            _logger = logger;
        }
        /// <inheritdoc/>
        public Task<bool> AddCourse(Class course)
        {
            try
            {
                _context.Classes.Add(course);
                _context.SaveChanges();
                return Task.FromResult(true);
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
        public Task<bool> DeleteCourse(int id)
        {
            try
            {
                Class? course = _context.Classes.Find(id);
                if (course != null)
                {
                    _context.Classes.Remove(course);
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
        public Task<Class?> Get(int id)
        {
            try
            {
                Class? course = _context.Classes.Find(id);
                return Task.FromResult(course);
            }
            catch (Exception ex)
            {
                Exception? loggedException = ex;
                while (loggedException.InnerException != null)
                {
                    loggedException = loggedException.InnerException;
                }
                _logger.LogError(loggedException.Message);
                return Task.FromResult<Class?>(null);
            }
        }

        /// <inheritdoc/>
        public Task<Class?> GetCourseForIdAndTeacher(int courseId, int teacherId)
        {
            try
            {
                Class? course = _context.Classes
                    .Include(c => c.Teacher)
                    .FirstOrDefault(c => c.Id == courseId && c.TeacherId == teacherId);
                return Task.FromResult(course);
            }
            catch (Exception ex)
            {
                Exception? loggedException = ex;
                while (loggedException.InnerException != null)
                {
                    loggedException = loggedException.InnerException;
                }
                _logger.LogError(loggedException.Message);
                return Task.FromResult<Class?>(null);
            }
        }

        /// <inheritdoc/>
        public Task<bool> UpdateCourse(Class course)
        {
            try
            {
                Class? existincClass = _context.Classes.Find(course.Id);
                if (existincClass != null)
                {
                    existincClass.Name = course.Name;
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
