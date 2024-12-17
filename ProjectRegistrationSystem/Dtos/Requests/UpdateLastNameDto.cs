using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to update a last name.
    /// </summary>
    public class UpdateLastNameDto
    {
        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Last name must be between 2 and 100 characters.")]
        public string LastName { get; set; }
    }
}