using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data.Entities;

namespace ProjectRegistrationSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Picture> Pictures { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<Person>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Address>()
                .HasKey(a => a.Id);

            modelBuilder.Entity<Picture>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Person)
                .WithOne(p => p.User)
                .HasForeignKey<Person>(p => p.UserId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.Address)
                .WithOne()
                .HasForeignKey<Person>(p => p.AddressId);

            modelBuilder.Entity<Person>()
                .HasOne(p => p.ProfilePicture)
                .WithOne()
                .HasForeignKey<Person>(p => p.ProfilePictureId);
        }
    }
}