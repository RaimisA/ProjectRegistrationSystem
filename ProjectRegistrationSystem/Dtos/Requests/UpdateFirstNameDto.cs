using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to update a first name.
    /// </summary>
    public class UpdateFirstNameDto
    {
        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "First name must be between 2 and 100 characters.")]
        public string FirstName { get; set; }
    }
}