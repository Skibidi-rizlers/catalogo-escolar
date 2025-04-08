namespace Catalogo_Escolar_API.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;
        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        public Task<bool> AddStudent(Student student)
        {
            try
            {
                student.CreatedAt = DateTime.Now;
                _context.Students.Add(student);
                _context.SaveChangesAsync();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        public Task<bool> ChangePasswordForStudent(string email, string newPassword)
        {
            try
            {
                Student? student = _context.Students.FirstOrDefault(s => s.Email == email);
                if (student != null)
                {
                    student.Password = newPassword;
                    student.UpdatedAt = DateTime.Now;
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

        public Task<Student?> GetStudent(string email, string password)
        {
            Student? student = _context.Students.FirstOrDefault(s => s.Email == email && s.Password == password);
            return Task.FromResult(student);
        }
    }
}
