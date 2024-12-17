using System.ComponentModel.DataAnnotations;
using ProjectRegistrationSystem.Validators;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to create or update a user.
    /// </summary>
    public class UserRequestDto
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        [Required]
        [StringLengthValidator(MinimumLength = 4, MaximumLength = 50)]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        [PasswordValidator(MinimumLength = 8, RequireUppercase = true, RequireLowercase = true, RequireDigit = true, RequireSpecialCharacter = true)]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the confirmation password.
        /// </summary>
        [Required]
        [CompareValidator(OtherProperty = "Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}