using Library.Common.Contacts;
using Library.Common.Seeds;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Model.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext(DbContextOptions<ApplicationDatabaseContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    Id = 1,
                    Name = RoleType.User,
                    NormalName = RoleDescription.Get(RoleType.User)
                },
                new Role
                {
                    Id = 2,
                    Name = RoleType.Admin,
                    NormalName = RoleDescription.Get(RoleType.Admin)
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
