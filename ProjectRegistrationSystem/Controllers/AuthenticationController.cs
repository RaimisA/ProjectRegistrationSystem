using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Services.Interfaces;
using System;
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
        private readonly ILogger<AuthenticationController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthenticationController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="jwtService">The JWT service.</param>
        /// <param name="logger">The logger.</param>
        public AuthenticationController(IUserService userService, IJwtService jwtService, ILogger<AuthenticationController> logger)
        {
            _userService = userService;
            _jwtService = jwtService;
            _logger = logger;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="userRequestDto">The user request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRequestDto userRequestDto)
        {
            _logger.LogInformation("Registering user with username: {Username}", userRequestDto.Username);

            try
            {
                var user = await _userService.RegisterUserAsync(userRequestDto.Username, userRequestDto.Password);
                _logger.LogInformation("User registered successfully with username: {Username}", userRequestDto.Username);
                return Ok(new { Message = "User registered successfully." });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError(ex, "Failed to register user with username: {Username}", userRequestDto.Username);
                return BadRequest(new { Message = ex.Message });
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="userLoginRequestDto">The user login request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequestDto userLoginRequestDto)
        {
            _logger.LogInformation("Logging in user with username: {Username}", userLoginRequestDto.Username);

            var (success, role) = await _userService.LoginAsync(userLoginRequestDto.Username, userLoginRequestDto.Password);
            if (!success)
            {
                _logger.LogWarning("Invalid login attempt for username: {Username}", userLoginRequestDto.Username);
                return Unauthorized("Invalid username or password.");
            }

            var user = await _userService.GetUserByUsernameAsync(userLoginRequestDto.Username);
            if (user == null)
            {
                _logger.LogWarning("User not found for username: {Username}", userLoginRequestDto.Username);
                return NotFound("User not found.");
            }

            var token = _jwtService.GetJwtToken(userLoginRequestDto.Username, role, user.Id);
            _logger.LogInformation("User logged in successfully with username: {Username}", userLoginRequestDto.Username);
            return Ok(new { Token = token, UserId = user.Id });
        }
    }
}