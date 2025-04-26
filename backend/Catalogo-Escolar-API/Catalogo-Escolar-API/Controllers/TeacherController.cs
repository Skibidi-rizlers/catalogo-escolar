using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo_Escolar_API.Controllers
{
    [Route("teacher")]
    [ApiController]
    public class TeacherController : Controller
    {
        private readonly ITeacherService _teacherService;
        private readonly ILogger<TeacherController> _logger;
        public TeacherController(ITeacherService teacherService, ILogger<TeacherController> logger)
        {
            _teacherService = teacherService;
            _logger = logger;
        }

        [HttpDelete("course")]
        public async Task<IActionResult> DeleteCourse([FromQuery] int courseId, [FromQuery] int teacherId)
        {
            if (courseId <= 0 || teacherId <= 0)
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.DeleteCourse(courseId, teacherId);
            if (result)
                return Ok("Course deleted");
            else
                return BadRequest("Failed to delete course");
        }


    }
}