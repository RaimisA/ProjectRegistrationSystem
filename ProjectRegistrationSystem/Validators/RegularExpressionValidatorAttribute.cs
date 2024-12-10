using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RegularExpressionValidatorAttribute : ValidationAttribute
    {
        public string Pattern { get; set; }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            }

            var stringValue = value.ToString();

            if (!System.Text.RegularExpressions.Regex.IsMatch(stringValue, Pattern))
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is not in the correct format.");
            }

            return ValidationResult.Success;
        }
    }
}