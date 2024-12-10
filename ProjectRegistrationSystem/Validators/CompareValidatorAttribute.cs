using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CompareValidatorAttribute : ValidationAttribute
    {
        public string OtherProperty { get; set; }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var otherProperty = validationContext.ObjectType.GetProperty(OtherProperty);
            if (otherProperty == null)
            {
                return new ValidationResult($"The {OtherProperty} field does not exist.");
            }

            var otherValue = otherProperty.GetValue(validationContext.ObjectInstance);

            if (!Equals(value, otherValue))
            {
                return new ValidationResult($"The {validationContext.DisplayName} field does not match the {OtherProperty} field.");
            }

            return ValidationResult.Success;
        }
    }
}