namespace ProjectRegistrationSystem.Dtos.Results
{
    /// <summary>
    /// Represents the result DTO for an address.
    /// </summary>
    public class AddressResultDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the address.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the house number of the address.
        /// </summary>
        public string HouseNumber { get; set; }

        /// <summary>
        /// Gets or sets the apartment number of the address.
        /// </summary>
        public string ApartmentNumber { get; set; }
    }
}