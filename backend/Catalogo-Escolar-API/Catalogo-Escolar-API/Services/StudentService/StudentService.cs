using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Service for managing students
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;
        /// <summary>
        /// Constructor for StudentService
        /// </summary>
        /// <param name="context">database context</param>
        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a new student to the database.
        /// </summary>
        /// <param name="data">Data of student</param>
        /// <returns>Result of operation</returns>
        public Task<bool> AddStudent(User data)
        {
            try
            {
                var user = _context.Users.Add(data);
                _context.Students.Add(new Student() { UserId = user.Entity.Id});
                _context.SaveChangesAsync();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }
        /// <summary>
        /// Changes the password of the student with the provided email.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="newPassword">New desired password</param>
        /// <returns>Result of operation</returns>
        public Task<bool> ChangePasswordForStudent(string email, string newPassword)
        {
            try
            {
                Student? student = _context.Students.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
                if (student != null)
                {
                    student.User.Password = newPassword;
                    _context.SaveChangesAsync();
                    return Task.FromResult(true);
                }
                return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        /// <summary>
        /// Returns a student with the provided email and password.
        /// </summary>
        /// <param name="email">Email</param>
        /// <param name="password">Password</param>
        /// <returns>Student if it exists</returns>
        public Task<Student?> GetStudent(string email, string password)
        {
            Student? student = _context.Students.Include(s => s.User).FirstOrDefault(s => s.User.Email == email && s.User.Password == password);
            return Task.FromResult(student);
        }
    }
}
