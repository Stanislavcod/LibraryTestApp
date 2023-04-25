using Library.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Model.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = "User",
                },
                new Role
                {
                    Id = 2,
                    Name = "Admin",
                });
            modelBuilder.Entity<Book>().HasData(
                new Book
                {
                    Id = 1,
                    Name = "The Great Gatsby",
                    Author = "F. Scott Fitzgerald",
                    Genre = "Classic literature",
                    Release = new DateTime(1925, 4, 10)
                },
               new Book
               {
                   Id = 2,
                   Name = "To Kill a Mockingbird",
                   Author = "Harper Lee",
                   Genre = "Classic literature",
                   Release = new DateTime(1960, 7, 11)
               });
        }

        public DbSet<Book> Book { get; set; } = default!;
        public DbSet<User> Users { get; set; } = default!;
    }
}
