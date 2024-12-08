using ProjectRegistrationSystem.Dtos.Results;

namespace ProjectRegistrationSystem.Dtos.Requests
{
    public class PersonResultDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressResultDto Address { get; set; }
        public PictureResultDto ProfilePicture { get; set; }
    }
}
