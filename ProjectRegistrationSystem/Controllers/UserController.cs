using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;
using ProjectRegistrationSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Controllers
{
    /// <summary>
    /// Controller for managing user-related operations.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IPersonMapper _personMapper;
        private readonly IAddressMapper _addressMapper;
        private readonly IPictureService _pictureService;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="personMapper">The person mapper.</param>
        /// <param name="addressMapper">The address mapper.</param>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="logger">The logger.</param>
        public UserController(IUserService userService, IPersonMapper personMapper, IAddressMapper addressMapper, IPictureService pictureService, ILogger<UserController> logger)
        {
            _userService = userService;
            _personMapper = personMapper;
            _addressMapper = addressMapper;
            _pictureService = pictureService;
            _logger = logger;
        }

        /// <summary>
        /// Adds person information for the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="personRequestDto">The person request DTO.</param>
        /// <param name="profilePicture">The profile picture file.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("addPersonInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPersonInfo(Guid userId, [FromForm] PersonRequestDto personRequestDto, IFormFile profilePicture)
        {
            _logger.LogInformation("Adding person information for user ID: {UserId}", userId);

            var person = _personMapper.Map(personRequestDto);
            if (profilePicture != null)
            {
                using var memoryStream = new MemoryStream();
                await profilePicture.CopyToAsync(memoryStream);
                var pictureRequestDto = new PictureRequestDto
                {
                    FileName = profilePicture.FileName,
                    Data = memoryStream.ToArray(),
                    ContentType = profilePicture.ContentType
                };
                var picture = await _pictureService.ProcessAndSavePictureAsync(pictureRequestDto);
                person.ProfilePicture = picture;
            }

            var result = await _userService.AddPersonInfoAsync(userId, person);
            if (!result)
            {
                _logger.LogError("Failed to add person information for user ID: {UserId}", userId);
                return BadRequest("Failed to add person information.");
            }

            _logger.LogInformation("Person information added successfully for user ID: {UserId}", userId);
            return Ok("Person information added successfully.");
        }

        /// <summary>
        /// Gets person information for the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("getPersonInfo")]
        public async Task<IActionResult> GetPersonInfo(Guid userId)
        {
            _logger.LogInformation("Getting person information for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var personResultDto = _personMapper.Map(person);
            _logger.LogInformation("Person information retrieved successfully for user ID: {UserId}", userId);
            return Ok(personResultDto);
        }

        /// <summary>
        /// Updates the first name of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="firstName">The new first name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateFirstName")]
        public async Task<IActionResult> UpdateFirstName(Guid userId, [FromBody] string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return BadRequest("First name cannot be empty or whitespace.");
            }

            _logger.LogInformation("Updating first name for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var result = await _userService.UpdateFirstNameAsync(person.Id, firstName);
            if (!result)
            {
                _logger.LogError("Failed to update first name for user ID: {UserId}", userId);
                return BadRequest("Failed to update first name.");
            }

            _logger.LogInformation("First name updated successfully for user ID: {UserId}", userId);
            return Ok("First name updated successfully.");
        }

        /// <summary>
        /// Updates the last name of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="lastName">The new last name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateLastName")]
        public async Task<IActionResult> UpdateLastName(Guid userId, [FromBody] string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("Last name cannot be empty or whitespace.");
            }

            _logger.LogInformation("Updating last name for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var result = await _userService.UpdateLastNameAsync(person.Id, lastName);
            if (!result)
            {
                _logger.LogError("Failed to update last name for user ID: {UserId}", userId);
                return BadRequest("Failed to update last name.");
            }

            _logger.LogInformation("Last name updated successfully for user ID: {UserId}", userId);
            return Ok("Last name updated successfully.");
        }

        /// <summary>
        /// Updates the personal code of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="personalCode">The new personal code.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updatePersonalCode")]
        public async Task<IActionResult> UpdatePersonalCode(Guid userId, [FromBody] string personalCode)
        {
            if (string.IsNullOrWhiteSpace(personalCode))
            {
                return BadRequest("Personal code cannot be empty or whitespace.");
            }

            _logger.LogInformation("Updating personal code for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var result = await _userService.UpdatePersonalCodeAsync(person.Id, personalCode);
            if (!result)
            {
                _logger.LogError("Failed to update personal code for user ID: {UserId}", userId);
                return BadRequest("Failed to update personal code.");
            }

            _logger.LogInformation("Personal code updated successfully for user ID: {UserId}", userId);
            return Ok("Personal code updated successfully.");
        }

        /// <summary>
        /// Updates the phone number of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updatePhoneNumber")]
        public async Task<IActionResult> UpdatePhoneNumber(Guid userId, [FromBody] string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Phone number cannot be empty or whitespace.");
            }

            _logger.LogInformation("Updating phone number for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var result = await _userService.UpdatePhoneNumberAsync(person.Id, phoneNumber);
            if (!result)
            {
                _logger.LogError("Failed to update phone number for user ID: {UserId}", userId);
                return BadRequest("Failed to update phone number.");
            }

            _logger.LogInformation("Phone number updated successfully for user ID: {UserId}", userId);
            return Ok("Phone number updated successfully.");
        }

        /// <summary>
        /// Updates the email of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="email">The new email.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateEmail")]
        public async Task<IActionResult> UpdateEmail(Guid userId, [FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be empty or whitespace.");
            }

            _logger.LogInformation("Updating email for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var result = await _userService.UpdateEmailAsync(person.Id, email);
            if (!result)
            {
                _logger.LogError("Failed to update email for user ID: {UserId}", userId);
                return BadRequest("Failed to update email.");
            }

            _logger.LogInformation("Email updated successfully for user ID: {UserId}", userId);
            return Ok("Email updated successfully.");
        }

        /// <summary>
        /// Updates the address of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="addressRequestDto">The new address request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateAddress")]
        public async Task<IActionResult> UpdateAddress(Guid userId, [FromBody] AddressRequestDto addressRequestDto)
        {
            _logger.LogInformation("Updating address for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            var address = _addressMapper.Map(addressRequestDto);
            var result = await _userService.UpdateAddressAsync(person.Id, address);
            if (!result)
            {
                _logger.LogError("Failed to update address for user ID: {UserId}", userId);
                return BadRequest("Failed to update address.");
            }

            _logger.LogInformation("Address updated successfully for user ID: {UserId}", userId);
            return Ok("Address updated successfully.");
        }

        /// <summary>
        /// Updates the profile picture of the specified user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="profilePicture">The new profile picture file.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateProfilePicture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfilePicture(Guid userId, IFormFile profilePicture)
        {
            if (profilePicture == null)
            {
                return BadRequest("Profile picture cannot be null.");
            }

            _logger.LogInformation("Updating profile picture for user ID: {UserId}", userId);

            var person = await _userService.GetPersonInfoByUserIdAsync(userId);
            if (person == null)
            {
                _logger.LogWarning("Person not found for user ID: {UserId}", userId);
                return NotFound("Person not found.");
            }

            using var memoryStream = new MemoryStream();
            await profilePicture.CopyToAsync(memoryStream);
            var pictureRequestDto = new PictureRequestDto
            {
                FileName = profilePicture.FileName,
                Data = memoryStream.ToArray(),
                ContentType = profilePicture.ContentType
            };
            var picture = await _pictureService.ProcessAndSavePictureAsync(pictureRequestDto, person.ProfilePicture?.Id);

            var result = await _userService.UpdateProfilePictureAsync(person.Id, picture);
            if (!result)
            {
                _logger.LogError("Failed to update profile picture for user ID: {UserId}", userId);
                return BadRequest("Failed to update profile picture.");
            }

            _logger.LogInformation("Profile picture updated successfully for user ID: {UserId}", userId);
            return Ok("Profile picture updated successfully.");
        }

        /// <summary>
        /// Deletes the specified user. (Admin only)
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpDelete("deleteUser")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            _logger.LogInformation("Deleting user ID: {UserId}", userId);

            var result = await _userService.DeleteUserAsync(userId);
            if (!result)
            {
                _logger.LogError("Failed to delete user ID: {UserId}", userId);
                return BadRequest("Failed to delete user.");
            }

            _logger.LogInformation("User deleted successfully for user ID: {UserId}", userId);
            return Ok("User deleted successfully.");
        }

        /// <summary>
        /// Updates the role of the specified user. (Admin only)
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="updateUserRoleDto">The DTO containing the new role.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateRole")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateRole(Guid userId, [FromBody] UpdateUserRoleDto updateUserRoleDto)
        {
            _logger.LogInformation("Updating role for user ID: {UserId}", userId);

            var result = await _userService.UpdateUserRoleAsync(userId, updateUserRoleDto.Role);
            if (!result)
            {
                _logger.LogError("Failed to update role for user ID: {UserId}", userId);
                return BadRequest("Failed to update role.");
            }

            _logger.LogInformation("Role updated successfully for user ID: {UserId}", userId);
            return Ok("Role updated successfully.");
        }

        /// <summary>
        /// Retrieves all users with their usernames and user IDs. (Admin only)
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("getAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Retrieving all users.");

            var users = await _userService.GetAllUsersAsync();
            var result = users.Select(u => new { u.Username, u.Id }).ToList();

            _logger.LogInformation("All users retrieved successfully.");
            return Ok(result);
        }
    }
}