namespace ProjectRegistrationSystem.Services.Interfaces
{
    /// <summary>
    /// Interface for generating JWT tokens.
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for the specified user.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="role">The role of the user.</param>
        /// <param name="userId">The unique identifier of the user.</param>
        /// <returns>The generated JWT token.</returns>
        string GetJwtToken(string username, string role, Guid userId);
    }
}