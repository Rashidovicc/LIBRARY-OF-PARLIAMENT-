using EFLibrary.Domain.Entities.Books;
using EFLibrary.Domain.Entities.Employes;
using EFLibrary.Domain.Entities.Libraries;
using EFLibrary.Domain.Entities.Orders;
using EFLibrary.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace EFLibrary.Data.Contexts
{
    public class LibraryDbContext : DbContext
    {
        //this class for connecting and managing the database with the EF Core
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Library> Libraries { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MyLibrary1;Username=postgres;Password=botir1202");
        }

       
    }
}