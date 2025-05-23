﻿using Catalogo_Escolar_API.Model.DTOs;
using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Catalogo_Escolar_API.Services.TeacherService
{
    /// <summary>
    /// Represents the teacher service class
    /// </summary>
    public class TeacherService : ITeacherService
    {
        private readonly SchoolContext _context;
        private readonly PasswordHasher<User> _passwordHasher;
        /// <summary>
        /// Represents the constructor of the teacher service class
        /// </summary>
        /// <param name="context">Context of database</param>
        public TeacherService(SchoolContext context)
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

        /// <inheritdoc/>
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

        /// <inheritdoc/>
        public Task<Teacher?> Get(string email)
        {
            Teacher? student = _context.Teachers.Include(s => s.User).FirstOrDefault(s => s.User.Email == email);
            return Task.FromResult(student);
        }

        /// <inheritdoc/>
        public Task<bool> DeleteCourse(string courseName, int teacherId)
        {
            try
            {
                var course = _context.Classes.FirstOrDefault(c => c.Name == courseName && c.TeacherId == teacherId);
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
                Console.WriteLine(ex.Message);
                return Task.FromResult(false);
            }
        }
        /// <inheritdoc/>
        public Task<bool> AddCourse(int teacherId, string courseName)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
                return Task.FromResult(false);
            var existingCourse = _context.Classes.FirstOrDefault(c => c.Name == courseName && c.TeacherId == teacherId);
            if (existingCourse != null)
                return Task.FromResult(false);
            try
            {
                var course = new Class()
                {
                    Name = courseName,
                    TeacherId = teacherId
                };
                _context.Classes.Add(course);
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
        public Task<bool> ModifyCourse(string oldCourseName, int teacherId, string courseName)
        {
            if (teacherId <= 0 ||
                string.IsNullOrEmpty(courseName) ||
                string.IsNullOrEmpty(oldCourseName) ||
                oldCourseName == courseName ||
                courseName == null ||
                oldCourseName == null ||
                _context.Teachers.FirstOrDefault(t => t.Id == teacherId) == null ||
                _context.Classes.FirstOrDefault(c => c.Name == courseName && c.TeacherId == teacherId) != null)
            {
                return Task.FromResult(false);
            }
            try
            {
                var course = _context.Classes.FirstOrDefault(c => c.Name == oldCourseName && c.TeacherId == teacherId);
                if (course != null)
                {
                    course.Name = courseName;
                    course.TeacherId = teacherId;
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
        public Task<bool> AddStudentToCourse(string studentName, string courseName)
        {
            //check if student exists
            var student = _context.Students.FirstOrDefault(s => s.User.FirstName+' '+s.User.LastName == studentName);
            if (student == null)
                return Task.FromResult(false);
            //check if course exists
            var course = _context.Classes.FirstOrDefault(c => c.Name == courseName);
            if (course == null)
                return Task.FromResult(false);
            //check if student is already in course
            var studentClass = _context.StudentClasses.FirstOrDefault(sc => sc.StudentId == student.Id && sc.ClassId == course.Id);
            if (studentClass != null)
                return Task.FromResult(false);
            //add student to course
            try
            {
                var newStudentClass = new StudentClass()
                {
                    StudentId = student.Id,
                    ClassId = course.Id,
                    EnrolledAt = DateTime.Now
                };
                _context.StudentClasses.Add(newStudentClass);
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
        public Task<bool> DeleteStudentFromCourse(string studentName, string courseName)
        {
            //check if student exists
            var student = _context.Students.FirstOrDefault(s => s.User.FirstName+' '+s.User.LastName == studentName);
            if (student == null)
                return Task.FromResult(false);
            //check if course exists
            var course = _context.Classes.FirstOrDefault(c => c.Name == courseName);
            if (course == null)
                return Task.FromResult(false);
            //check if student is already in course
            var studentClass = _context.StudentClasses.FirstOrDefault(sc => sc.StudentId == student.Id && sc.ClassId == course.Id);
            if (studentClass == null)
                return Task.FromResult(false);
            //remove student from course
            try
            {
                _context.StudentClasses.Remove(studentClass);
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
        public Task<List<ClassDTO>> GetTeacherCourses(int teacherId)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
                return Task.FromResult(new List<ClassDTO>());

            var courses = _context.Classes
                .Where(c => c.TeacherId == teacherId)
                .ToList();

            var courseDTOs = new List<ClassDTO>();
            foreach (var course in courses)
            {
                var courseDTO = new ClassDTO
                {
                    Name = course.Name,
                    Students = _context.StudentClasses
                        .Where(sc => sc.ClassId == course.Id)
                        .Select(sc => new StudentDTO
                        {
                            Name = sc.Student.User.FirstName + " " + sc.Student.User.LastName
                        }).ToList()
                };
                courseDTOs.Add(courseDTO);
            }
            return Task.FromResult(courseDTOs);
        }

        ///<inheritdoc/>
        public Task<List<StudentDTO>> GetStudents()
        {
            var students = _context.Students
                .Include(s => s.User)
                .Select(s => new StudentDTO
                {
                   Name = s.User.FirstName + " " + s.User.LastName,
                })
                .ToList();
            return Task.FromResult(students);
        }

        /// <inheritdoc/>
        public Task<ClassDTO?> GetTeacherCourse(int teacherId, string courseName)
        {
            var teacher = _context.Teachers.FirstOrDefault(t => t.Id == teacherId);
            if (teacher == null)
                return Task.FromResult<ClassDTO?>(null);

            var course = _context.Classes
                .FirstOrDefault(c => c.Name == courseName && c.TeacherId == teacherId);

            if (course == null)
                return Task.FromResult<ClassDTO?>(null);

            var courseDTO = new ClassDTO
            {
                Name = course.Name,
                Students = _context.StudentClasses
                    .Where(sc => sc.ClassId == course.Id)
                    .Select(sc => new StudentDTO
                    {
                        Name = sc.Student.User.FirstName + " " + sc.Student.User.LastName
                    }).ToList()
            };

            return Task.FromResult<ClassDTO?>(courseDTO);
        }

    }
}
