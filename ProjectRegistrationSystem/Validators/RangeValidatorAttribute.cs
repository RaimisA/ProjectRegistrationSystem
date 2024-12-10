using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class RangeValidatorAttribute : ValidationAttribute
    {
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult($"The {validationContext.DisplayName} field is required.");
            }

            if (value is int intValue)
            {
                if (intValue < Minimum || intValue > Maximum)
                {
                    return new ValidationResult($"The {validationContext.DisplayName} field must be between {Minimum} and {Maximum}.");
                }
            }
            else
            {
                return new ValidationResult($"The {validationContext.DisplayName} field must be a number.");
            }

            return ValidationResult.Success;
        }
    }
}