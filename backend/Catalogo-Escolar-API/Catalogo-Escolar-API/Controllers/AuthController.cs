using Catalogo_Escolar_API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Catalogo_Escolar_API.Controllers
{
    /// <summary>
    /// Represents the authentication controller. It is used to authenticate users and any actions it may include, such as JWT creation.
    /// </summary>
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Represents the default constructor of the <see cref="AuthController"/> class.
        /// </summary>
        public AuthController() { }

        /// <summary>
        /// Authenticates the user and generates a JWT token.
        /// </summary>
        /// <param name="model">Login parameters.</param>
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Authentication successful.</response>
        /// <response code="400">Data in body is not correct.</response>
        /// <response code="422">Could not login.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(422)]
        public async Task<ActionResult<string>> Login([FromBody] LoginModel model)
        {
            try
            {
                return Ok("This is your JWT");
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Failed to log in user.");
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
        public async Task<ActionResult<bool>> Register()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception ex)
            {
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
        public async Task<ActionResult<bool>> ChangePassword([FromBody] ChangePasswordModel model)
        {
            try
            {
                return Ok(true);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Failed to change password.");
            }
        }
    }
}
