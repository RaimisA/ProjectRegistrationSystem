using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Results
{
    public class PersonRequestDto
    {
        [Required]
        [StringLengthValidator(MinimumLength = 2, MaximumLength = 100)]
        public string FirstName { get; set; }

        [Required]
        [StringLengthValidator(MinimumLength = 2, MaximumLength = 100)]
        public string LastName { get; set; }

        [Required]
        [RegularExpressionValidator(Pattern = @"^\d{11}$", ErrorMessage = "Personal code must be 11 digits.")]
        public string PersonalCode { get; set; }

        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid E.164 format.")]
        public string PhoneNumber { get; set; }

        [Required]
        [EmailAddress]
        [EmailDomainValidator]
        public string Email { get; set; }

        [Required]
        public AddressRequestDto Address { get; set; }
    }
}