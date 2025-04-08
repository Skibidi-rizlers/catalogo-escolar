using Catalogo_Escolar_API.Helpers;
using Catalogo_Escolar_API.Services.StudentService;

namespace Catalogo_Escolar_API.Services.AuthService
{
    /// <summary>
    /// Represents the authentication service. It is used to authenticate users and any actions it may include.
    /// </summary>
    public class AuthService : IAuthService
    {
        private readonly IStudentService _studentService;
        private readonly JWTGenerator _jwtGenerator;
        /// <summary>
        /// Constructor for AuthService
        /// </summary>
        /// <param name="studentService"></param>
        /// <param name="jWTGenerator"></param>
        public AuthService(IStudentService studentService, JWTGenerator jWTGenerator)
        {
            _studentService = studentService;
            _jwtGenerator = jWTGenerator;
        }
        /// <summary>
        /// Changes the password of the user.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="oldPassword">Old password of user</param>
        /// <param name="newPassword">New desire password of user</param>
        /// <returns>Result of operation</returns>
        public async Task<bool> ChangePassword(string email, string oldPassword, string newPassword)
        {
            throw new NotImplementedException("Change password not implemented yet.");
        }

        /// <summary>
        /// Returns a JWT token if the user is authenticated.
        /// </summary>
        /// <param name="email">Email of user</param>
        /// <param name="password">Password of user</param>
        /// <returns>JWT</returns>
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
                    generatedToken = _jwtGenerator.GenerateTokenForStudent(student);
                }
                return generatedToken;
            }
            else if (email.EndsWith("@teacher.com"))
            {
                throw new NotImplementedException("Login for teacher not implemented yet.");
            }

            return generatedToken;
        }
    }
}
