using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class UpdatePhoneNumberDto
    {
        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid E.164 format.")]
        public string PhoneNumber { get; set; }
    }
}
