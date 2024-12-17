using Microsoft.EntityFrameworkCore;
using ProjectRegistrationSystem.Data.Entities;

namespace ProjectRegistrationSystem.Data
{
    /// <summary>
    /// Represents the application database context.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Gets or sets the DbSet for users.
        /// </summary>
        public DbSet<User> Users { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for persons.
        /// </summary>
        public DbSet<Person> Persons { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for addresses.
        /// </summary>
        public DbSet<Address> Addresses { get; set; }

        /// <summary>
        /// Gets or sets the DbSet for pictures.
        /// </summary>
        public DbSet<Picture> Pictures { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by a <see cref="DbContext"/>.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        /// <summary>
        /// Configures the schema needed for the context.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
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