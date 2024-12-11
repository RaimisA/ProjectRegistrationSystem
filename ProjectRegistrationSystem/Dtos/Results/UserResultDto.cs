namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents the result DTO for a user.
    /// </summary>
    public class UserResultDto
    {
        /// <summary>
        /// Gets or sets the unique identifier for the user.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the role of the user.
        /// </summary>
        public string Role { get; set; }
    }
}