using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to validate the length of a string.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StringLengthValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the minimum length of the string.
        /// </summary>
        public int MinimumLength { get; set; }

        /// <summary>
        /// Gets or sets the maximum length of the string.
        /// </summary>
        public int MaximumLength { get; set; }

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
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            }

            var stringValue = value.ToString();

            if (stringValue.Length < MinimumLength)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field must be at least {MinimumLength} characters long.");
            }

            if (stringValue.Length > MaximumLength)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field must be at most {MaximumLength} characters long.");
            }

            return ValidationResult.Success;
        }
    }
}