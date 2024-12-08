using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using ProjectRegistrationSystem.Services.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Services
{
    /// <summary>
    /// Service for managing user-related operations.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPersonRepository _personRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPictureRepository _pictureRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        /// <param name="personRepository">The person repository.</param>
        /// <param name="addressRepository">The address repository.</param>
        /// <param name="pictureRepository">The picture repository.</param>
        public UserService(IUserRepository userRepository, IPersonRepository personRepository, IAddressRepository addressRepository, IPictureRepository pictureRepository)
        {
            _userRepository = userRepository;
            _personRepository = personRepository;
            _addressRepository = addressRepository;
            _pictureRepository = pictureRepository;
        }

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The registered user.</returns>
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

        /// <summary>
        /// Creates a new user entity.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>The created user entity.</returns>
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

        /// <summary>
        /// Creates a password hash and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="passwordSalt">The password salt.</param>
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        /// <summary>
        /// Logs in a user.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>A result indicating whether the login was successful and the user's role.</returns>
        public async Task<(bool Success, string Role)> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null || !VerifyPasswordHash(password, Convert.FromBase64String(user.PasswordHash), Convert.FromBase64String(user.PasswordSalt)))
            {
                return (false, null);
            }

            return (true, user.Role);
        }

        /// <summary>
        /// Verifies the password hash.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="passwordHash">The password hash.</param>
        /// <param name="passwordSalt">The password salt.</param>
        /// <returns>True if the password hash is valid; otherwise, false.</returns>
        public bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }

        /// <summary>
        /// Adds person information for a user.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <param name="person">The person entity.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Gets person information by ID.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonInfoAsync(Guid id)
        {
            return await _personRepository.GetPersonByIdAsync(id);
        }

        /// <summary>
        /// Gets person information by user ID.
        /// </summary>
        /// <param name="userId">The user ID.</param>
        /// <returns>The person entity.</returns>
        public async Task<Person> GetPersonInfoByUserIdAsync(Guid userId)
        {
            return await _personRepository.GetPersonByUserIdAsync(userId);
        }

        /// <summary>
        /// Updates the first name of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="firstName">The new first name.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the last name of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="lastName">The new last name.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the personal code of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="personalCode">The new personal code.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the phone number of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="phoneNumber">The new phone number.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the email of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="email">The new email.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the address of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="address">The new address.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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

        /// <summary>
        /// Updates the profile picture of a person.
        /// </summary>
        /// <param name="id">The person ID.</param>
        /// <param name="picture">The new profile picture.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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
                await _personRepository.SaveChangesAsync();
            }

            return true;
        }

        /// <summary>
        /// Deletes a user by ID.
        /// </summary>
        /// <param name="id">The user ID.</param>
        /// <returns>True if the operation was successful; otherwise, false.</returns>
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
                    _pictureRepository.DeletePicture(person.ProfilePicture);
                }
                if (person.Address != null)
                {
                    _addressRepository.DeleteAddress(person.Address);
                }
                _personRepository.DeletePerson(person);
            }

            await _userRepository.DeleteUserAsync(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Gets a user by username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The user entity.</returns>
        public async Task<User> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetUserByUsernameAsync(username);
        }
    }
}