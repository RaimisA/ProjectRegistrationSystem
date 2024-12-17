using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to update an email address.
    /// </summary>
    public class UpdateEmailDto
    {
        /// <summary>
        /// Gets or sets the email address.
        /// </summary>
        [Required]
        [EmailAddress]
        [EmailDomainValidator]
        public string Email { get; set; }
    }
}