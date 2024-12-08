using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data;
using ProjectRegistrationSystem.Data.Entities;
using ProjectRegistrationSystem.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace ProjectRegistrationSystem.Repositories
{
    public class PictureRepository : IPictureRepository
    {
        private readonly ApplicationDbContext _context;

        public PictureRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Picture> GetPictureByIdAsync(Guid id)
        {
            return await _context.Pictures.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddPictureAsync(Picture picture)
        {
            await _context.Pictures.AddAsync(picture);
        }

        public void DeletePicture(Picture picture)
        {
            _context.Pictures.Remove(picture);
        }

        public async Task UpdatePictureAsync(Picture picture)
        {
            _context.Pictures.Update(picture);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}