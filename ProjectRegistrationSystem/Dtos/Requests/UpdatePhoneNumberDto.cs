using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to update a phone number.
    /// </summary>
    public class UpdatePhoneNumberDto
    {
        /// <summary>
        /// Gets or sets the phone number.
        /// </summary>
        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid E.164 format.")]
        public string PhoneNumber { get; set; }
    }
}