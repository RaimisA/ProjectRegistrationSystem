using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to compare two properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class CompareValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets the other property to compare with.
        /// </summary>
        public string OtherProperty { get; set; }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
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