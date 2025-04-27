using Catalogo_Escolar_API.Model.Payloads.Auth;
using Catalogo_Escolar_API.Services.AuthService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;

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
                if (!model.IsValid())
                {
                    _logger.LogInformation("Login request result for " + model.ToString() + $": invalid data in body.");
                    return BadRequest("Invalid data in body.");
                }

                string? token = await _authService.Login(model);
                if (string.IsNullOrEmpty(token))
                {
                    _logger.LogWarning("Login request failure for " + model.Email);
                    return Unauthorized("Failed to login user.");
                }

                _logger.LogInformation("Login request successful for " + model.Email);
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

                if (!model.IsValid())
                {
                    _logger.LogInformation("Register request result for " + model.ToString() + $": invalid data in body.");
                    return BadRequest("Invalid data in body.");
                }

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

        /// <summary>
        /// Sends the reset password email to the user.
        /// </summary>
        /// <param name="model">Payload</param>
        /// <returns>A boolean indicating whether the email was sent.</returns>
        /// <response code="200">Send successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="422">Unable to send password.</response>
        [HttpPost("request-reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<bool>> ResetPasswordRequest([FromBody] ResetPasswordRequestPayload model)
        {
            try
            {
                var email = model.Email;

                if (string.IsNullOrEmpty(email))
                {
                    _logger.LogInformation("Request reset password result for " + model.ToString() + $": invalid data in body.");
                    return BadRequest("Email not valid.");
                }


                var result = await _authService.ResetPasswordRequest(email);
                _logger.LogInformation("Request Reset password result for " + model.ToString() + $": {result}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return UnprocessableEntity("Failed to Request Reset password.");
            }
        }

        /// <summary>
        /// Reset password of user.
        /// </summary>
        /// <param name="model">Payload</param>
        /// <returns>A boolean indicating whether the password was reset.</returns>
        /// <response code="200">Reset successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="422">Unable to reset password.</response>
        [HttpPost("reset-password")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<bool>> ResetPassword([FromBody] ResetPasswordPayload model)
        {
            try
            {
                if (!model.IsValid())
                {
                    _logger.LogInformation("Reset password request result for " + model.ToString() + $": invalid data in body.");
                    return BadRequest("Invalid data in body.");
                }

                var decodedBytes = Convert.FromBase64String(model.EncodedId);
                var decodedId = Encoding.UTF8.GetString(decodedBytes);
                int id = int.Parse(decodedId);

                var result = await _authService.ResetPassword(id, model.Password);
                _logger.LogInformation("Reset password result for " + model.ToString() + $": {result}.");
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return UnprocessableEntity("Failed to Reset password.");
            }
        }
    }
}
