using System.ComponentModel.DataAnnotations;
using ProjectRegistrationSystem.Validators;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class UserRequestDto
    {
        [Required]
        [StringLengthValidator(MinimumLength = 4, MaximumLength = 50)]
        public string Username { get; set; }

        [Required]
        [PasswordValidator(MinimumLength = 8, RequireUppercase = true, RequireLowercase = true, RequireDigit = true, RequireSpecialCharacter = true)]
        public string Password { get; set; }

        [Required]
        [CompareValidator(OtherProperty = "Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}