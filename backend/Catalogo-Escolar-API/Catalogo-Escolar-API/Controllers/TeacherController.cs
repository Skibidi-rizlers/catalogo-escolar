using Catalogo_Escolar_API.Services.StudentService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> DeleteCourse([FromQuery] string courseName)
        {
            try
            {
                var teacherEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                Teacher? teacher = await _teacherService.Get(teacherEmail);
                if (teacher == null)
                    return BadRequest("Teacher data is invalid");
                var teacherId = teacher.Id;
                var result = await _teacherService.DeleteCourse(courseName, teacherId);
                if (result)
                    return Ok("Course deleted");
                else
                    return BadRequest("Failed to delete course");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("add-course")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> AddCourse([FromQuery] string courseName)
        {
            try
            {
                var teacherEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                Teacher? teacher = await _teacherService.Get(teacherEmail);
                if (teacher == null)
                    return BadRequest("Teacher data is invalid");
                var teacherId = teacher.Id;
                var result = await _teacherService.AddCourse(teacherId, courseName);
                if (result)
                    return Ok("Course added");
                else
                    return BadRequest("Failed to add course");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPatch("modify-course")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> ModifyCourse([FromQuery] string courseName, [FromQuery] string oldCourseName)
        {
            try
            {
                var teacherEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                Teacher? teacher = await _teacherService.Get(teacherEmail);
                if (teacher == null)
                    return BadRequest("Teacher data is invalid");
                var teacherId = teacher.Id;
                var result = await _teacherService.ModifyCourse(oldCourseName, teacherId, courseName);
                if (result)
                    return Ok("Course modified");
                else
                    return BadRequest("Failed to modify course");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("get-teacher-courses")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherCourses()
        {
            try
            {
                var teacherEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                Teacher? teacher = await _teacherService.Get(teacherEmail);
                if (teacher == null)
                    return BadRequest("Teacher data is invalid");
                var teacherId = teacher.Id;
                var result = await _teacherService.GetTeacherCourses(teacherId);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No courses found for this teacher");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }
        [HttpGet("get-teacher-course")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetTeacherCourse([FromQuery] string courseName)
        {
            try
            {
                var teacherEmail = User.FindFirst(ClaimTypes.Email)?.Value;
                Teacher? teacher = await _teacherService.Get(teacherEmail);
                if (teacher == null)
                    return BadRequest("Teacher data is invalid");
                var teacherId = teacher.Id;
                var result = await _teacherService.GetTeacherCourse(teacherId, courseName);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound("No courses found for this teacher");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpPost("add-student-to-course")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> AddStudentToCourse([FromQuery] string studentName, [FromQuery] string courseName)
        {
            if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(courseName))
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.AddStudentToCourse(studentName, courseName);
            if (result)
                return Ok("Student added to course");
            else
                return BadRequest("Failed to add student to course");
        }

        [HttpDelete("delete-student-from-course")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> DeleteStudentFromCourse([FromQuery] string studentName, [FromQuery] string courseName)
        {
            if (string.IsNullOrEmpty(studentName) || string.IsNullOrEmpty(courseName))
                return BadRequest("Course data is incorrect");
            var result = await _teacherService.DeleteStudentFromCourse(studentName, courseName);
            if (result)
                return Ok("Student deleted from course");
            else
                return BadRequest("Failed to delete student from course");
        }

        [HttpGet("get-students")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetStudents()
        {
            var result = await _teacherService.GetStudents();
            if (result != null)
                return Ok(result);
            else
                return NotFound("No students found");
        }
    }
}