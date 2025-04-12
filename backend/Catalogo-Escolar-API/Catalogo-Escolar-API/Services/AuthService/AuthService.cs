using Catalogo_Escolar_API.Helpers;
using Catalogo_Escolar_API.Model.DTO;
using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Identity;

namespace Catalogo_Escolar_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly PasswordHasher<User> _passwordHasher;
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly JWTGenerator _jwtGenerator;

        public AuthService(IStudentService studentService, ITeacherService teacherService, JWTGenerator jWTGenerator)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _jwtGenerator = jWTGenerator;
            _passwordHasher = new();
        }

        public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword)) 
            { 
                return false; 
            }

            if(oldPassword == newPassword)
            {
                return false;
            }

            if (email.EndsWith("@student.com"))
            {
                Student? student = await _studentService.Get(email);
                if (student != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(student.User, student.User.Password, oldPassword);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return await _studentService.ChangePassword(email, newPassword);
                    }
                }
                return false;
            }
            else if (email.EndsWith("@teacher.com"))
            {
                Teacher? teacher = await _teacherService.Get(email);
                if (teacher != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(teacher.User, teacher.User.Password, oldPassword);
                    if (result == PasswordVerificationResult.Success)
                    {
                        return await _teacherService.ChangePassword(email, newPassword);
                    }
                    return false;
                }
            }

            return false;
        }

        public async Task<string?> Login(LoginDTO loginDTO)
        {
            string? generatedToken = null;

            if(string.IsNullOrEmpty(loginDTO.Email) || string.IsNullOrEmpty(loginDTO.Password))
            {
                return generatedToken;
            }

            if (loginDTO.Email.EndsWith("@student.com"))
            {
                Student? student = await _studentService.Get(loginDTO.Email);
                if (student != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(student.User, student.User.Password, loginDTO.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        generatedToken = _jwtGenerator.GenerateToken(student.User);
                    }
                }
                return generatedToken;
            }
            else if (loginDTO.Email.EndsWith("@teacher.com"))
            {
                Teacher? teacher = await _teacherService.Get(loginDTO.Email);
                if (teacher != null)
                {
                    var result = _passwordHasher.VerifyHashedPassword(teacher.User, teacher.User.Password, loginDTO.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        generatedToken = _jwtGenerator.GenerateToken(teacher.User);
                    }
                }
                return generatedToken;
            }

            return generatedToken;
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            if (registerDTO.RoleName != "student" && registerDTO.RoleName != "teacher")
                return false;

            User newUser = new()
            {
                FirstName = registerDTO.FirstName,
                LastName = registerDTO.LastName,
                Password = registerDTO.Password,
                Email = EmailFormatter.GetEmail(registerDTO)
            };

            newUser.Password = _passwordHasher.HashPassword(newUser, registerDTO.Password);
            if (registerDTO.RoleName == "student")
            {
                await _studentService.Add(newUser);
                return true;
            }
            else if(registerDTO.RoleName == "teacher")
            {
                await _teacherService.Add(newUser);
                return true;
            }

            return false;
        }
    }
}
