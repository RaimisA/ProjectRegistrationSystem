using System.ComponentModel.DataAnnotations;
using ProjectRegistrationSystem.Validators;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class AddressRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "City name must be between 2 and 100 characters.")]
        public string City { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Street name must be between 2 and 100 characters.")]
        public string Street { get; set; }

        [Required]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "House number must be between 1 and 10 characters.")]
        public string HouseNumber { get; set; }

        [StringLength(10, ErrorMessage = "Apartment number must be at most 10 characters.")]
        public string ApartmentNumber { get; set; }
    }
}