using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to create or update a person.
    /// </summary>
    public class PersonRequestDto
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        [Required]
        [StringLengthValidator(MinimumLength = 2, MaximumLength = 100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        [Required]
        [StringLengthValidator(MinimumLength = 2, MaximumLength = 100)]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the personal code of the person.
        /// </summary>
        [Required]
        [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal code must be 11 digits.")]
        public string PersonalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        [Required]
        [RegularExpression(@"^\+?[1-9]\d{1,14}$", ErrorMessage = "Phone number must be a valid E.164 format.")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the person.
        /// </summary>
        [Required]
        [EmailAddress]
        [EmailDomainValidator]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        [Required]
        public AddressRequestDto Address { get; set; }
    }
}