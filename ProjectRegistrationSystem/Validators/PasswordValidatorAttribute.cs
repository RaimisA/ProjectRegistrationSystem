using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to validate password strength.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class PasswordValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum length of the password.
        /// </summary>
        public int MinimumLength { get; set; } = 4;

        /// <summary>
        /// Gets or sets the maximum length of the password.
        /// </summary>
        public int MaximumLength { get; set; } = 60;

        /// <summary>
        /// Gets or sets a value indicating whether the password requires an uppercase letter.
        /// </summary>
        public bool RequireUppercase { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the password requires a lowercase letter.
        /// </summary>
        public bool RequireLowercase { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the password requires a digit.
        /// </summary>
        public bool RequireDigit { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether the password requires a special character.
        /// </summary>
        public bool RequireSpecialCharacter { get; set; } = false;

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
                return new ValidationResult("Password is required.");
            }

            var password = value.ToString();

            if (string.IsNullOrEmpty(password))
            {
                return new ValidationResult("Password is required.");
            }

            if (password.Length < MinimumLength)
            {
                return new ValidationResult($"Password must be at least {MinimumLength} characters long.");
            }

            if (password.Length > MaximumLength)
            {
                return new ValidationResult($"Password must be at most {MaximumLength} characters long.");
            }

            if (RequireUppercase && !password.Any(char.IsUpper))
            {
                return new ValidationResult("Password must contain at least one uppercase letter.");
            }

            if (RequireLowercase && !password.Any(char.IsLower))
            {
                return new ValidationResult("Password must contain at least one lowercase letter.");
            }

            if (RequireDigit && !password.Any(char.IsDigit))
            {
                return new ValidationResult("Password must contain at least one digit.");
            }

            if (RequireSpecialCharacter && password.All(char.IsLetterOrDigit))
            {
                return new ValidationResult("Password must contain at least one special character.");
            }

            return ValidationResult.Success;
        }
    }
}