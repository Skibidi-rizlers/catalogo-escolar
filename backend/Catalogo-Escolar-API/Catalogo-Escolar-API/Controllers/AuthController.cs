using Catalogo_Escolar_API.Model.DTO;
using Catalogo_Escolar_API.Services.AuthService;
using Catalogo_Escolar_API.Services.BackupService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Catalogo_Escolar_API.Controllers
{
    /// <summary>
    /// Represents the authentication controller. It is used to authenticate users and any actions it may include, such as JWT creation.
    /// </summary>
    [ApiController]
    [Route("auth")]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        /// <summary>
        /// Represents the default constructor of the <see cref="AuthController"/> class.
        /// </summary>
        public AuthController(IAuthService authService, ILogger<AuthController> logger) 
        { 
            _authService = authService;
            _logger = logger;
        }

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="model">Login parameters.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Authentication successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="401">Could not log in user.</response>
        /// <response code="500">Server side error.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        [AllowAnonymous]
        public async Task<ActionResult<string>> Login([FromBody] LoginPayload model)
        {
            try
            {
                _logger.LogInformation("Login request received for " + model.Email);
                string? token = await _authService.Login(model);
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("Login request failure for " + model.Email);
                    return Unauthorized("Failed to login user.");
                }
                _logger.LogInformation("Login request successful for " + model.Email + ": " + token);
                return Ok(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500, "Internal server error.");
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>A boolean indicating whether the registration was successful.</returns>
        /// <response code="200">Registration successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="422">Could not register.</response>
        [HttpPost("register")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        [AllowAnonymous]
        public async Task<ActionResult<string?>> Register([FromBody] RegisterPayload model)
        {
            try
            {
                _logger.LogInformation("Register request received for " + model.ToString());
                var result = await _authService.Register(model);
                _logger.LogInformation("Register request result for " + model.ToString() + $": {result ?? "failure"}");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return UnprocessableEntity("Failed to register user.");
            }
        }

        /// <summary>
        /// Changes the password for the current user.
        /// </summary>
        /// <param name="model">Change password parameters.</param>
        /// <returns>A boolean indicating whether the change was successful.</returns>
        /// <response code="200">Change successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="422">Unable to change password.</response>
        [HttpPost("change-password")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordPayload model)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogInformation("Change password request result for " + model.ToString() + $": missing data in JWT.");
                    return BadRequest("User not authenticated.");
                }

                _logger.LogInformation("Change password request result for " + email + " " + model.ToString());

                var result = await _authService.ChangePassword(email, model.OldPassword, model.NewPassword);
                _logger.LogInformation("Change password request result for " + model.ToString() + $": {result}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return UnprocessableEntity("Failed to change password.");
            }
        }
    }
}
