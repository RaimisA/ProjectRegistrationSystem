using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class UpdateEmailDto
    {
        [Required]
        [EmailAddress]
        [EmailDomainValidator]
        public string Email { get; set; }
    }
}
