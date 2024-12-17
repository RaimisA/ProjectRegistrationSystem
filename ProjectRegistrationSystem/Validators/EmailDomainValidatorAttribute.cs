using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to check if an email has a valid domain.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class EmailDomainValidatorAttribute : ValidationAttribute
    {
        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            var email = value as string;
            if (!string.IsNullOrEmpty(email))
            {
                var atIndex = email.IndexOf('@');
                if (atIndex == -1)
                    return new ValidationResult("Invalid email address.");

                var tldIndex = email.LastIndexOf('.');
                if (tldIndex == -1)
                    return new ValidationResult("Invalid top-level domain.");

                if (tldIndex <= atIndex + 1)
                    return new ValidationResult("Invalid top-level domain.");

                if (tldIndex == email.Length - 1)
                    return new ValidationResult("Missing top-level domain.");
            }

            return ValidationResult.Success!;
        }
    }
}