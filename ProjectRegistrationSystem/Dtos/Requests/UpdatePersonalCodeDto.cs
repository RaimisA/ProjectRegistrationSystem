using ProjectRegistrationSystem.Dtos.Requests;
using System.ComponentModel.DataAnnotations;

public class UpdatePersonalCodeDto
{
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "Personal code must be 11 digits.")]
    public string PersonalCode { get; set; }
}