using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Models.Models;

namespace PredmetProjekat.Repositories.Context
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Register>().HasIndex(x => x.RegisterCode).IsUnique();

            modelBuilder.Entity<Account>()
                .HasDiscriminator<string>("AccountType")
                .HasValue<Account>("account")
                .HasValue<Admin>("admin")
                .HasValue<Employee>("employee");

        }
    }
}
