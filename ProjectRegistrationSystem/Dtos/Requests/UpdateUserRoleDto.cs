using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class UpdateUserRoleDto
    {
        [Required]
        [RoleValidator]
        public string Role { get; set; }
    }
}