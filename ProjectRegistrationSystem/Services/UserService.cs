using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using ProjectRegistrationSystem.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPictureRepository _pictureRepository;

        public UserService(IUserRepository userRepository, IPersonRepository personRepository, IAddressRepository addressRepository, IPictureRepository pictureRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _pictureRepository = pictureRepository;
        }

        public async Task<User> RegisterUserAsync(string username, string password)
        {
            var existingUser = await _userRepository.GetUserByUsernameAsync(username);
            if (existingUser != null)
            {
                throw new InvalidOperationException("User already exists.");
            }

            var user = CreateUser(username, password);
            await _userRepository.AddUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return user;
        }

        public async Task CheckPersonInfoAsync(string personalCode, string phoneNumber, string email)
        {
            var existingPerson = await _personRepository.GetPersonByPersonalCodeAsync(personalCode);
            if (existingPerson != null)
            {
                throw new InvalidOperationException("Personal code already exists.");
            }

            existingPerson = await _personRepository.GetPersonByPhoneNumberAsync(phoneNumber);
            if (existingPerson != null)
            {
                throw new InvalidOperationException("Phone number already exists.");
            }

            existingPerson = await _personRepository.GetPersonByEmailAsync(email);
            if (existingPerson != null)
            {
                throw new InvalidOperationException("Email already exists.");
            }
        }

        public User CreateUser(string username, string password)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = username,
                PasswordHash = Convert.ToBase64String(passwordHash),
                PasswordSalt = Convert.ToBase64String(passwordSalt),
                Role = "User"
            };

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<(bool Success, string Role)> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
            {
                return (false, null);
            }

            return (true, user.Role);
        }

        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        public async Task<bool> AddPersonInfoAsync(Guid userId, Person person)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null || user.Person != null)
            {
                return false;
            }
            person.UserId = userId;
            user.Person = person;
            await _personRepository.AddPersonAsync(person);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<Person> GetPersonInfoAsync(Guid id)
        {
            return await _personRepository.GetPersonByIdAsync(id);
        }

        public async Task<Person> GetPersonInfoByUserIdAsync(Guid userId)
        {
            return await _personRepository.GetPersonByUserIdAsync(userId);
        }

        public async Task<bool> UpdateFirstNameAsync(Guid id, string firstName)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.FirstName = firstName;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateLastNameAsync(Guid id, string lastName)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.LastName = lastName;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePersonalCodeAsync(Guid id, string personalCode)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.PersonalCode = personalCode;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdatePhoneNumberAsync(Guid id, string phoneNumber)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.PhoneNumber = phoneNumber;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateEmailAsync(Guid id, string email)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.Email = email;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAddressAsync(Guid id, Address address)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }
            person.Address.City = address.City;
            person.Address.Street = address.Street;
            person.Address.HouseNumber = address.HouseNumber;
            person.Address.ApartmentNumber = address.ApartmentNumber;
            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateProfilePictureAsync(Guid id, Picture picture)
        {
            var person = await _personRepository.GetPersonByIdAsync(id);
            if (person == null)
            {
                return false;
            }

            var existingPicture = person.ProfilePicture;
            if (existingPicture != null)
            {
                existingPicture.FileName = picture.FileName;
                existingPicture.Data = picture.Data;
                existingPicture.ContentType = picture.ContentType;
                existingPicture.Width = picture.Width;
                existingPicture.Height = picture.Height;
                await _pictureRepository.UpdatePictureAsync(existingPicture);
            }
            else
            {
                picture.Id = Guid.NewGuid();
                person.ProfilePicture = picture;
                await _pictureRepository.AddPictureAsync(picture);
            }

            await _personRepository.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return false;
            }

            var person = user.Person;
            if (person != null)
            {
                if (person.ProfilePicture != null)
                {
                    await _pictureRepository.DeletePictureAsync(person.ProfilePicture);
                }
                if (person.Address != null)
                {
                    await _addressRepository.DeleteAddressAsync(person.Address);
                }
            }

            await _userRepository.DeleteUserAsync(user);
            await _userRepository.SaveChangesAsync();

            return true;
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }

        public async Task<bool> UpdateUserRoleAsync(Guid userId, string role)
        {
            role = char.ToUpper(role[0]) + role.Substring(1).ToLower();

            await _userRepository.UpdateUserRoleAsync(userId, role);
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllUsersAsync();
        }
    }
}