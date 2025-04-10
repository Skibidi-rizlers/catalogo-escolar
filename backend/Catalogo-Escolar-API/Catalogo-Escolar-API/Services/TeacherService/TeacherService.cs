﻿using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly SchoolContext _context;
        public TeacherService(SchoolContext context)
        {
            _context = context;
        }
        public Task<bool> Add(User data)
        {
            try
            {
                var user = _context.Users.Add(data);
                _context.Teachers.Add(new Teacher() { UserId = user.Entity.Id });
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
                Teacher? teacher = _context.Teachers.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
                if (teacher != null)
                {
                    teacher.User.Password = newPassword;
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

        public Task<Teacher?> Get(string email, string password)
        {
            Teacher? student = _context.Teachers.Include(s => s.User).FirstOrDefault(s => s.User.Email == email && s.User.Password == password);
            return Task.FromResult(student);
        }
    }
}
