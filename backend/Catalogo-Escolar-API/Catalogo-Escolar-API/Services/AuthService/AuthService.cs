using Catalogo_Escolar_API.Helpers;
using Catalogo_Escolar_API.Model.DTO;
using Catalogo_Escolar_API.Services.StudentService;

namespace Catalogo_Escolar_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IStudentService _studentService;
        private readonly ITeacherService _teacherService;
        private readonly JWTGenerator _jwtGenerator;

        public AuthService(IStudentService studentService, ITeacherService teacherService, JWTGenerator jWTGenerator)
        {
            _studentService = studentService;
            _teacherService = teacherService;
            _jwtGenerator = jWTGenerator;
        }

        public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException("Change password not implemented yet.");
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
                Student? student = await _studentService.Get(loginDTO.Email, loginDTO.Password);
                if (student != null)
                {
                    generatedToken = _jwtGenerator.GenerateToken(student.User);
                }
                return generatedToken;
            }
            else if (loginDTO.Email.EndsWith("@teacher.com"))
            {
                Teacher? teacher = await _teacherService.Get(loginDTO.Email, loginDTO.Password);
                if (teacher != null)
                {
                    generatedToken = _jwtGenerator.GenerateToken(teacher.User);
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
