using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class StringLengthValidatorAttribute : ValidationAttribute
    {
        public int MinimumLength { get; set; }
        public int MaximumLength { get; set; }

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