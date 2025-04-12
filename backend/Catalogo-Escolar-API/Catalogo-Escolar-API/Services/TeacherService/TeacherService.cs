using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        public TeacherService(SchoolContext context)
        {
            _context = context;
            _passwordHasher = new();
        }
        public Task<bool> Add(User data)
        {
            try
            {
                var user = _context.Users.Add(data);
                _context.SaveChanges();
                _context.Teachers.Add(new Teacher() { UserId = user.Entity.Id });
                _context.SaveChanges();
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
                Teacher? teacher = _context.Teachers.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
                if (teacher != null)
                {
                    teacher.User.Password = _passwordHasher.HashPassword(teacher.User, newPassword);
                    _context.SaveChanges();
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

        public Task<Teacher?> Get(string email)
        {
            Teacher? student = _context.Teachers.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
            return Task.FromResult(student);
        }
    }
}
