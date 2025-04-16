﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.StudentService
{
    /// <summary>
    /// Represents the Student service class
    /// </summary>
    public class StudentService : IStudentService
    {
        private readonly SchoolContext _context;
        private readonly PasswordHasher<User> _passwordHasher;

        /// <summary>
        /// The student service constructor
        /// </summary>
        /// <param name="context">Context of database</param>
        public StudentService(SchoolContext context)
        {
            _context = context;
            _passwordHasher = new();
        }

        /// <inheritdoc/>
        public Task<bool> Add(User data)
        {
            try
            {
                var user = _context.Users.Add(data);
                _context.SaveChanges();
                _context.Students.Add(new Student() { UserId = user.Entity.Id});
                _context.SaveChanges();
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }

        /// <inheritdoc/>
        public Task<bool> ChangePassword(string email, string newPassword)
        {
            try
            {
                Student? student = _context.Students.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
                if (student != null)
                {
                    student.User.Password = _passwordHasher.HashPassword(student.User, newPassword);
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

        /// <inheritdoc/>
        public Task<Student?> Get(string email)
        {
            Student? student = _context.Students.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
            return Task.FromResult(student);
        }
    }
}
