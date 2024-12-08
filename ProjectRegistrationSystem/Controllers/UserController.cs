using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;
using ProjectRegistrationSystem.Services;
using ProjectRegistrationSystem.Services.Interfaces;
using System;
using System.IO;
using System.Security.Claims;
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
        private readonly IPictureMapper _pictureMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserController"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="personMapper">The person mapper.</param>
        /// <param name="addressMapper">The address mapper.</param>
        /// <param name="pictureService">The picture service.</param>
        /// <param name="pictureMapper">The picture mapper.</param>
        public UserController(IUserService userService, IPersonMapper personMapper, IAddressMapper addressMapper, IPictureService pictureService, IPictureMapper pictureMapper)
        {
            _userService = userService;
            _personMapper = personMapper;
            _addressMapper = addressMapper;
            _pictureService = pictureService;
            _pictureMapper = pictureMapper;
        }

        /// <summary>
        /// Adds person information for the authenticated user.
        /// </summary>
        /// <param name="personRequestDto">The person request DTO.</param>
        /// <param name="profilePicture">The profile picture file.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPost("addPersonInfo")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPersonInfo([FromForm] PersonRequestDto personRequestDto, IFormFile profilePicture)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userIdClaim == null)
            {
                return Unauthorized("User ID claim not found.");
            }

            var userId = new Guid(userIdClaim);
            var person = _personMapper.Map(personRequestDto);

            // Process and save the profile picture
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
                return BadRequest("Failed to add person information.");
            }
            return Ok("Person information added successfully.");
        }

        /// <summary>
        /// Gets person information by ID.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPersonInfo(Guid id)
        {
            var person = await _userService.GetPersonInfoAsync(id);
            if (person == null)
            {
                return NotFound("Person not found.");
            }
            var personResultDto = _personMapper.Map(person);
            return Ok(personResultDto);
        }

        /// <summary>
        /// Updates the first name of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="firstName">The new first name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateFirstName")]
        public async Task<IActionResult> UpdateFirstName(Guid id, [FromBody] string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
            {
                return BadRequest("First name cannot be empty or whitespace.");
            }
            var result = await _userService.UpdateFirstNameAsync(id, firstName);
            if (!result)
            {
                return BadRequest("Failed to update first name.");
            }
            return Ok("First name updated successfully.");
        }

        /// <summary>
        /// Updates the last name of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="lastName">The new last name.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateLastName")]
        public async Task<IActionResult> UpdateLastName(Guid id, [FromBody] string lastName)
        {
            if (string.IsNullOrWhiteSpace(lastName))
            {
                return BadRequest("Last name cannot be empty or whitespace.");
            }
            var result = await _userService.UpdateLastNameAsync(id, lastName);
            if (!result)
            {
                return BadRequest("Failed to update last name.");
            }
            return Ok("Last name updated successfully.");
        }

        /// <summary>
        /// Updates the personal code of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="personalCode">The new personal code.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updatePersonalCode")]
        public async Task<IActionResult> UpdatePersonalCode(Guid id, [FromBody] string personalCode)
        {
            if (string.IsNullOrWhiteSpace(personalCode))
            {
                return BadRequest("Personal code cannot be empty or whitespace.");
            }
            var result = await _userService.UpdatePersonalCodeAsync(id, personalCode);
            if (!result)
            {
                return BadRequest("Failed to update personal code.");
            }
            return Ok("Personal code updated successfully.");
        }

        /// <summary>
        /// Updates the phone number of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updatePhoneNumber")]
        public async Task<IActionResult> UpdatePhoneNumber(Guid id, [FromBody] string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return BadRequest("Phone number cannot be empty or whitespace.");
            }
            var result = await _userService.UpdatePhoneNumberAsync(id, phoneNumber);
            if (!result)
            {
                return BadRequest("Failed to update phone number.");
            }
            return Ok("Phone number updated successfully.");
        }

        /// <summary>
        /// Updates the email of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="email">The new email.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateEmail")]
        public async Task<IActionResult> UpdateEmail(Guid id, [FromBody] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email cannot be empty or whitespace.");
            }
            var result = await _userService.UpdateEmailAsync(id, email);
            if (!result)
            {
                return BadRequest("Failed to update email.");
            }
            return Ok("Email updated successfully.");
        }

        /// <summary>
        /// Updates the address of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="addressRequestDto">The new address request DTO.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateAddress")]
        public async Task<IActionResult> UpdateAddress(Guid id, [FromBody] AddressRequestDto addressRequestDto)
        {
            var address = _addressMapper.Map(addressRequestDto);
            var result = await _userService.UpdateAddressAsync(id, address);
            if (!result)
            {
                return BadRequest("Failed to update address.");
            }
            return Ok("Address updated successfully.");
        }

        /// <summary>
        /// Updates the profile picture of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="profilePicture">The new profile picture file.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpPut("updateProfilePicture")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateProfilePicture(Guid id, IFormFile profilePicture)
        {
            if (profilePicture == null)
            {
                return BadRequest("Profile picture cannot be null.");
            }

            using var memoryStream = new MemoryStream();
            await profilePicture.CopyToAsync(memoryStream);
            var pictureRequestDto = new PictureRequestDto
            {
                FileName = profilePicture.FileName,
                Data = memoryStream.ToArray(),
                ContentType = profilePicture.ContentType
            };
            var picture = await _pictureService.ProcessAndSavePictureAsync(pictureRequestDto);

            var result = await _userService.UpdateProfilePictureAsync(id, picture);
            if (!result)
            {
                return BadRequest("Failed to update profile picture.");
            }
            return Ok("Profile picture updated successfully.");
        }

        /// <summary>
        /// Gets the profile picture of a person by ID.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpGet("{id}/profilePicture")]
        public async Task<IActionResult> GetProfilePicture(Guid id)
        {
            var person = await _userService.GetPersonInfoAsync(id);
            if (person == null || person.ProfilePicture == null)
            {
                return NotFound("Profile picture not found.");
            }

            var pictureResultDto = _pictureMapper.Map(person.ProfilePicture);
            return File(pictureResultDto.Data, pictureResultDto.ContentType, pictureResultDto.FileName);
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (!result)
            {
                return BadRequest("Failed to delete user.");
            }
            return Ok("User deleted successfully.");
        }
    }
}