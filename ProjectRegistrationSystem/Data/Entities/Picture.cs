using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    /// <summary>
    /// Represents a picture entity.
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Gets or sets the unique identifier for the picture.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the file name of the picture.
        /// </summary>
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the data of the picture.
        /// </summary>
        [Required]
        public byte[] Data { get; set; }

        /// <summary>
        /// Gets or sets the content type of the picture.
        /// </summary>
        [Required]
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the width of the picture.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// Gets or sets the height of the picture.
        /// </summary>
        public int Height { get; set; }
    }
}