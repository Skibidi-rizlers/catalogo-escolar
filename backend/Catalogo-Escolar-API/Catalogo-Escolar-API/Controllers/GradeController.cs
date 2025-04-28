using Catalogo_Escolar_API.Model.DTOs;
using Catalogo_Escolar_API.Services.GradeService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo_Escolar_API.Controllers
{
    [ApiController]
    [Route("grade")]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;
        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        [HttpPost("post")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> AddGrade([FromBody] GradeDTO grade)
        {
            try
            {
                if (grade == null)
                {
                    return BadRequest("Grade data is invalid");
                }
                var result = await _gradeService.Add(grade);

                if (result)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }

        [HttpGet("get")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> GetGrades([FromQuery] int assignmentId)
        {
            try
            {
                var result = await _gradeService.GetGradesForAssignment(assignmentId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }


        [HttpPost("post-multiple")]
        [Authorize(Roles = "teacher")]
        public async Task<IActionResult> AddGrades([FromBody] List<GradeDTO> grades)
        {
            try
            {
                if (grades == null || grades.Count == 0)
                {
                    return BadRequest("Grades data is invalid");
                }

                var result = await _gradeService.AddGrades(grades);
                if (result)
                {
                    return Ok();
                }

                return BadRequest("Error adding grades");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
