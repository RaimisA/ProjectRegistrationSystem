using ProjectRegistrationSystem.Data.Entities;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories.Interfaces
{
    public interface IPictureRepository
    {
        Task<Picture> GetPictureByIdAsync(Guid id);
        Task AddPictureAsync(Picture picture);
        void DeletePicture(Picture picture);
        Task UpdatePictureAsync(Picture picture);
        Task SaveChangesAsync();
    }
}