using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to check if a file exceeds the maximum allowed size.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private readonly int _maxFileSize;

        /// <summary>
        /// Initializes a new instance of the <see cref="MaxFileSizeAttribute"/> class.
        /// </summary>
        /// <param name="maxFileSize">The maximum allowed file size in bytes.</param>
        public MaxFileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
            {
                return ValidationResult.Success;
            }
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }
            if (file.Length > _maxFileSize)
            {
                return new ValidationResult($"File size should not be larger than {_maxFileSize} bytes");
            }
            return ValidationResult.Success;
        }
    }
}