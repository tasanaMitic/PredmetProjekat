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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
            modelBuilder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            
        }
    }
}
