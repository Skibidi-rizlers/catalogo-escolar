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

        [HttpDelete("remove-student-class")]
        public async Task<IActionResult> RemoveStudentFromClass([FromQuery] int studentId, [FromQuery] int classId)
        {
            if (studentId <= 0)
                return BadRequest("Student data is incorrect");
            var result = await _studentService.DeleteStudentFromClass(studentId, classId);
            if (result)
                return Ok("Student removed from class");
            else
                return BadRequest("Failed to remove student from class");
        }
    }
}
