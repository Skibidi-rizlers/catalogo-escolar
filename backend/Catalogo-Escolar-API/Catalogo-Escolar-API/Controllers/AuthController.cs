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
        /// <returns>A JWT token if authentication is successful.</returns>
        /// <response code="200">Authentication successful.</response>
        /// <response code="400">Error.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<string>> Login()
        {
            try
            {
                return Ok("This is your JWT");
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to log in user.");
            }
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <returns>A boolean indicating whether the registration was successful.</returns>
        /// <response code="200">Registration successful.</response>
        /// <response code="400">Error.</response>
        [HttpPost("register")]
        public async Task<ActionResult<bool>> Register()
        {
            try
            {
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest("Failed to register user.");
            }
        }
    }
}
