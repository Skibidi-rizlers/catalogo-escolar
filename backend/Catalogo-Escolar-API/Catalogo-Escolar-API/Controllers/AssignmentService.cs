using Catalogo_Escolar_API.Model;
using Catalogo_Escolar_API.Model.Payloads.Assignment;
using Catalogo_Escolar_API.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Catalogo_Escolar_API.Controllers
{
    /// <summary>
    /// Represents the Assignment controller
    /// </summary>
    [Route("assignment")]
    [ApiController]
    public class AssignmentController : ControllerBase
    {
        private readonly IAssignmentService _assignmentService;
        /// <summary>
        /// Constructor of Assignment controller
        /// </summary>
        /// <param name="assignmentService"></param>
        public AssignmentController(IAssignmentService assignmentService)
        {
            _assignmentService = assignmentService;
        }

        /// <summary>
        /// Creates a new assignment with data provided in body
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPost("post")]
        [Authorize(Roles = "teacher")]
        public async Task<ActionResult<bool>> CreateAssignment([FromBody] CreateAssignmentPayload payload)
        {
            return Ok(true);
            try
            {
                var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                Assignment newAssignment = new Assignment
                {
                    ClassId = payload.ClassId,
                    Title = payload.Title,
                    Description = payload.Description,
                    DueDate = payload.DueDate
                };

                //TODO: get class by id
                //TODO: check if class teacher id is the same as the one in the request (don't allow teachers to create assignments for other classes)

                bool result = await _assignmentService.AddAssignment(newAssignment);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the assignment with id provided in query parameters
        /// </summary>
        /// <param name="assignmentId"></param>
        /// <returns>Result of operation</returns>
        [HttpDelete("delete")]
        [Authorize(Roles = "teacher")]
        public async Task<ActionResult<bool>> DeleteAssignment([FromQuery] int assignmentId)
        {
            try
            {
                var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                //TODO: get class by id
                //TODO: check if class teacher id is the same as the one in the request (don't allow teachers to create assignments for other classes)

                bool result = await _assignmentService.DeleteAssignment(assignmentId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /// <summary>
        /// Updates the assignment with data provided in body
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        [HttpPatch("update")]
        [Authorize(Roles = "teacher")]
        public async Task<ActionResult<bool>> UpdateAssignment([FromBody] UpdateAssignmentPayload payload)
        {
            try
            {
                var teacherId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

                Assignment updatedAssignment = new Assignment
                {
                    Id = payload.Id,
                    Title = payload.Title,
                    Description = payload.Description,
                    DueDate = payload.DueDate
                };

                //TODO: get class by id
                //TODO: check if class teacher id is the same as the one in the request (don't allow teachers to create assignments for other classes)

                bool result = await _assignmentService.UpdateAssignment(updatedAssignment);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}