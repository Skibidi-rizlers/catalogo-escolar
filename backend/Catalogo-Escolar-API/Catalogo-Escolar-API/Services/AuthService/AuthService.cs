using Catalogo_Escolar_API.Services.StudentService;

namespace Catalogo_Escolar_API.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly IStudentService _studentService;
        public AuthService(IStudentService studentService)
        {
            _studentService = studentService;
        }
        public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(oldPassword) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            
            }

            if (email.EndsWith("@student.com"))
            {
                bool result = await _studentService.ChangePasswordForStudent(email, newPassword);
                return result;
            }
            else if (email.EndsWith("@teacher.com"))
            {
                throw new NotImplementedException("Change password for teacher not implemented yet.");
            }

            throw new Exception("Invalid email.");
        }

        public async Task<string?> Login(string email, string password)
        {
            string? generatedToken = null;

            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return generatedToken;
            }

            if (email.EndsWith("@student.com"))
            {
                Student? student = await _studentService.GetStudent(email, password);
                if (student != null)
                {
                    generatedToken = "STUDENTJWT";
                }
                return generatedToken;
            }
            else if (email.EndsWith("@teacher.com"))
            {
                throw new NotImplementedException("Login for teacher not implemented yet.");
            }

            throw new Exception("Invalid email.");
        }
    }
}
