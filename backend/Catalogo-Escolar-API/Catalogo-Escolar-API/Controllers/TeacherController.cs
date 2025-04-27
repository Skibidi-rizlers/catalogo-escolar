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

        [HttpDelete("delete-course")]
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

        [HttpPost("add-course")]
        public async Task<IActionResult> AddCourse([FromQuery] int teacherId, [FromQuery] string courseName)
        {
            if (teacherId <= 0 || string.IsNullOrEmpty(courseName))
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.AddCourse(teacherId, courseName);
            if (result)
                return Ok("Course added");
            else
                return BadRequest("Failed to add course");
        }

        [HttpPatch("modify-course")]
        public async Task<IActionResult> ModifyCourse([FromQuery] int teacherId, [FromQuery] string courseName, [FromQuery] string oldCourseName)
        {
            if (teacherId <= 0 || string.IsNullOrEmpty(courseName) || string.IsNullOrEmpty(oldCourseName))
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.ModifyCourse(oldCourseName, teacherId, courseName);
            if (result)
                return Ok("Course modified");
            else
                return BadRequest("Failed to modify course");
        }

        [HttpPost("add-student-to-course")]
        public async Task<IActionResult> AddStudentToCourse([FromQuery] int studentId, [FromQuery] int courseId)
        {
            if (studentId <= 0 || courseId <= 0)
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.AddStudentToCourse(studentId, courseId);
            if (result)
                return Ok("Student added to course");
            else
                return BadRequest("Failed to add student to course");
        }

        [HttpDelete("delete-student-from-course")]
        public async Task<IActionResult> DeleteStudentFromCourse([FromQuery] int studentId, [FromQuery] int courseId)
        {
            if (studentId <= 0 || courseId <= 0)
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.DeleteStudentFromCourse(studentId, courseId);
            if (result)
                return Ok("Student deleted from course");
            else
                return BadRequest("Failed to delete student from course");
        }

    }
}