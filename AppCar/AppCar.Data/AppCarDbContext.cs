using AppCar.Entities.EntityModels;
using System.Data.Entity;

namespace AppCar.Data
{
    public class AppCarDbContext : DbContext
    {
        public AppCarDbContext() : base("AppCarPetProject")
        {

        }
        public DbSet<Car> Cars { get; set; }

        public DbSet<Part> Parts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Sale> Sales { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Login> Logins { get; set; }

        public DbSet<Log> Logs { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>().HasMany<Part>(car => car.Parts).WithMany(part => part.Cars);
            modelBuilder.Entity<Supplier>().HasMany<Part>(supplier => supplier.Parts).WithRequired(part => part.Supplier).WillCascadeOnDelete(true);
        }
    }
}
