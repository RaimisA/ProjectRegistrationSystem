using ProjectRegistrationSystem.Validators;
using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    /// <summary>
    /// Represents a request to create or update a picture.
    /// </summary>
    public class PictureRequestDto
    {
        /// <summary>
        /// Gets or sets the file name of the picture.
        /// </summary>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the data of the picture.
        /// </summary>
        [Required]
        [MaxFileSize(10 * 1024 * 1024)]
        [AllowedExtensions(new[] { ".jpg", ".jpeg", ".png", ".webp", ".bmp", ".tiff" })]
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the content type of the picture.
        /// </summary>
        [Required]
        public string ContentType { get; set; }
    }
}