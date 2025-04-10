using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.StudentService
{
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;

        public StudentService(SchoolContext context)
        {
            _context = context;
        }

        public Task<bool> Add(User data)
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

        public Task<bool> ChangePassword(string email, string newPassword)
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

        public Task<Student?> Get(string email, string password)
        {
            Student? student = _context.Students.Include(s => s.User).FirstOrDefault(s => s.User.Email == email && s.User.Password == password);
            return Task.FromResult(student);
        }
    }
}
