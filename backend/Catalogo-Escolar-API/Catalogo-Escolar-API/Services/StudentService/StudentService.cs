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
        /// <param name="student">Student</param>
        /// <returns>Result of operation</returns>
        public Task<bool> AddStudent(User student)
        {
            try
            {
                _context.Users.Add(student);
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
                User? student = _context.Users.FirstOrDefault(s => s.Email == email);
                if (student != null)
                {
                    student.Password = newPassword;
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
        public Task<User?> GetStudent(string email, string password)
        {
            User? student = _context.Users.FirstOrDefault(s => s.Email == email && s.Password == password);
            return Task.FromResult(student);
        }
    }
}
