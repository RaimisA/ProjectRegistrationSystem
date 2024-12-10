using Microsoft.AspNetCore.Mvc;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Services.Interfaces;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Controllers
{
    /// <summary>
    /// Controller for managing authentication-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="jwtService">The JWT service.</param>
        public AuthenticationController(IUserService userService, IJwtService jwtService)
        {
            _userService = userService;
            _jwtService = jwtService;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userRequestDto">The user request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestDto userRequestDto)
        {
            try
            {
                var user = await _userService.RegisterUserAsync(userRequestDto.Username, userRequestDto.Password);
                return Ok("User registered successfully.");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userRequestDto">The user request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserRequestDto userRequestDto)
        {
            var (success, role) = await _userService.LoginAsync(userRequestDto.Username, userRequestDto.Password);
            if (!success)
            {
                return Unauthorized("Invalid username or password.");
            }

            var user = await _userService.GetUserByUsernameAsync(userRequestDto.Username);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var token = _jwtService.GetJwtToken(userRequestDto.Username, role, user.Id);
            return Ok(new { Token = token, UserId = user.Id });
        }
    }
}