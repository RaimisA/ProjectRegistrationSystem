using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to validate user roles.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RoleValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Role is required.");
            }

            var role = value.ToString();

            if (string.IsNullOrEmpty(role))
            {
                return new ValidationResult("Role is required.");
            }

            if (!string.Equals(role, "Admin", StringComparison.OrdinalIgnoreCase) && !string.Equals(role, "User", StringComparison.OrdinalIgnoreCase))
            {
                return new ValidationResult("Role must be either Admin or User.");
            }

            return ValidationResult.Success!;
        }
    }
}