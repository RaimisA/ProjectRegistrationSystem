using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    /// <summary>
    /// Represents an address entity.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [Required]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        [Required]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number of the address.
        /// </summary>
        [Required]
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the apartment number of the address.
        /// </summary>
        public string ApartmentNumber { get; set; }
    }
}