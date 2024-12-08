using ProjectRegistrationSystem.Dtos.Requests;

namespace ProjectRegistrationSystem.Dtos.Results
{
    public class PersonRequestDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCode { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public AddressRequestDto Address { get; set; }
    }
}
