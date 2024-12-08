using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    public class Picture
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public byte[] Data { get; set; }
        [Required]
        public string ContentType { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}