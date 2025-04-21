using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo_Escolar_API.Controllers
{
    [Route("student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private readonly IStudentService _studentService;
        private readonly ILogger<StudentController> _logger;
        public StudentController(IStudentService studentService, ILogger<StudentController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }

        [HttpPost("classes")]
        public async Task<IActionResult> GetClasses([FromBody] User user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email))
                return BadRequest("User data is incorrect");

            var classes = await _studentService.GetClasses(user.Email);
            return Ok(classes);
        }


    }
}
