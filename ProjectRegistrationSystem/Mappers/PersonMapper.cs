using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Dtos.Requests;
using ProjectRegistrationSystem.Dtos.Results;
using ProjectRegistrationSystem.Mappers.Interfaces;

namespace ProjectRegistrationSystem.Mappers
{
    /// <summary>
    /// Mapper for converting between Person entities and DTOs.
    /// </summary>
    public class PersonMapper : IPersonMapper
    {
        private readonly IAddressMapper _addressMapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PersonMapper"/> class.
        /// </summary>
        /// <param name="addressMapper">The address mapper.</param>
        public PersonMapper(IAddressMapper addressMapper)
        {
            _addressMapper = addressMapper;
        }

        /// <summary>
        /// Maps a PersonRequestDto to a Person entity.
        /// </summary>
        /// <param name="dto">The PersonRequestDto.</param>
        /// <returns>The Person entity.</returns>
        public Person Map(PersonRequestDto dto)
        {
            return new Person
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PersonalCode = dto.PersonalCode,
                PhoneNumber = dto.PhoneNumber,
                Email = dto.Email,
                Address = _addressMapper.Map(dto.Address)
            };
        }

        /// <summary>
        /// Maps a Person entity to a PersonResultDto.
        /// </summary>
        /// <param name="entity">The Person entity.</param>
        /// <returns>The PersonResultDto.</returns>
        public PersonResultDto Map(Person entity)
        {
            return new PersonResultDto
            {
                Id = entity.Id,
                FirstName = entity.FirstName,
                LastName = entity.LastName,
                PersonalCode = entity.PersonalCode,
                PhoneNumber = entity.PhoneNumber,
                Email = entity.Email,
                Address = _addressMapper.Map(entity.Address),
                ProfilePicture = entity.ProfilePicture != null ? new PictureResultDto
                {
                    Id = entity.ProfilePicture.Id,
                    FileName = entity.ProfilePicture.FileName,
                    Data = entity.ProfilePicture.Data,
                    ContentType = entity.ProfilePicture.ContentType
                } : null
            };
        }
    }
}