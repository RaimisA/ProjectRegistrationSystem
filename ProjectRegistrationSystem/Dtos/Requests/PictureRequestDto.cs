using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Results
{
    public class PictureRequestDto
    {
        [Required]
        public string FileName { get; set; }

        [Required]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }
    }
}
