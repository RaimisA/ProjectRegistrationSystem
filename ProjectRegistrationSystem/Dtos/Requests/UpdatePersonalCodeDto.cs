using ProjectRegistrationSystem.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

public class UpdatePersonalCodeDto
{
    /// <summary>
    /// Gets or sets the personal code.
    /// </summary>
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal code must be 11 digits.")]
    public string PersonalCode { get; set; }
}