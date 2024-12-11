using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents the result DTO for a person.
    /// </summary>
    public class PersonResultDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the personal code of the person.
        /// </summary>
        public string PersonalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the person.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address of the person.
        /// </summary>
        public AddressResultDto Address { get; set; }

        /// <summary>
        /// Gets or sets the profile picture of the person.
        /// </summary>
        public PictureResultDto ProfilePicture { get; set; }
    }
}