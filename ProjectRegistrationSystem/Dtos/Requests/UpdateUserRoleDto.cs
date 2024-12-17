using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to update a user's role.
    /// </summary>
    public class UpdateUserRoleDto
    {
        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        [Required]
        [RoleValidator]
        public string Role { get; set; }
    }
}