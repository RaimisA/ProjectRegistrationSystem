using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class PictureRequestDto
    {
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FileName { get; set; }

        [Required]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png" })]
        public byte[] Data { get; set; }

        [Required]
        public string ContentType { get; set; }
    }
}
