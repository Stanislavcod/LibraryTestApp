using Library.Model.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Model.DatabaseContext
{
    public class ApplicationDatabaseContext : DbContext
    {
        public ApplicationDatabaseContext (DbContextOptions<ApplicationDatabaseContext> options) 
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Book> Book { get; set; } = default!;
    }
}
