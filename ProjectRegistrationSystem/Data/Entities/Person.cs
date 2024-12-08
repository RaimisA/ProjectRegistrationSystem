using System.ComponentModel.DataAnnotations;

namespace ProjectRegistrationSystem.Data.Entities
{
    public class Person
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string PersonalCode { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Guid AddressId { get; set; }
        public Address Address { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ProfilePictureId { get; set; }
        public Picture ProfilePicture { get; set; }
    }
}