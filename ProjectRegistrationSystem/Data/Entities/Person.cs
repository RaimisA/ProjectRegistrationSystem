using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    /// <summary>
    /// Represents a person entity.
    /// </summary>
    public class Person
    {
        /// <summary>
        /// Gets or sets the unique identifier for the person.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the personal code of the person.
        /// </summary>
        [Required]
        public string PersonalCode { get; set; }

        /// <summary>
        /// Gets or sets the phone number of the person.
        /// </summary>
        [Required]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets the email of the person.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the address ID associated with the person.
        /// </summary>
        [Required]
        public Guid AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address associated with the person.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with the person.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user associated with the person.
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Gets or sets the profile picture ID associated with the person.
        /// </summary>
        public Guid ProfilePictureId { get; set; }

        /// <summary>
        /// Gets or sets the profile picture associated with the person.
        /// </summary>
        public Picture ProfilePicture { get; set; }
    }
}