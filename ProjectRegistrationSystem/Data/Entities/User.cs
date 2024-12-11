using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    /// <summary>
    /// Represents a user entity.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash of the user.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the password salt of the user.
        /// </summary>
        [Required]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        [Required]
        public string Role { get; set; } = "User";

        /// <summary>
        /// Gets or sets the person associated with the user.
        /// </summary>
        public Person Person { get; set; }
    }
}