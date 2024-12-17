using System.ComponentModel.DataAnnotations;
using ProjectRegistrationSystem.Validators;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to create or update an address.
    /// </summary>
    public class AddressRequestDto
    {
        /// <summary>
        /// Gets or sets the city name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "City name can only contain letters and numbers.")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street name.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Street name must be between 2 and 100 characters.")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Street name can only contain letters and numbers.")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number.
        /// </summary>
        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "House number must be between 1 and 10 characters.")]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the apartment number.
        /// </summary>
        [StringLength(10, ErrorMessage = "Apartment number must be at most 10 characters.")]
        public string ApartmentNumber { get; set; }
    }
}