using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Validators
{
    /// <summary>
    /// Validation attribute to check if a file has an allowed extension.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public class AllowedExtensionsAttribute : ValidationAttribute
    {
        private readonly string[] _extensions;

        /// <summary>
        /// Initializes a new instance of the <see cref="AllowedExtensionsAttribute"/> class.
        /// </summary>
        /// <param name="extensions">The allowed file extensions.</param>
        public AllowedExtensionsAttribute(string[] extensions)
        {
            _extensions = extensions;
        }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>An instance of the <see cref="ValidationResult"/> class.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is not IFormFile file)
            {
                return ValidationResult.Success;
            }

            var extension = Path.GetExtension(file.FileName);
            if (!_extensions.Contains(extension.ToLower()))
            {
                return new ValidationResult($"File extension not allowed. Allowed extensions: {string.Join(", ", _extensions)}");
            }

            return ValidationResult.Success;
        }
    }
}