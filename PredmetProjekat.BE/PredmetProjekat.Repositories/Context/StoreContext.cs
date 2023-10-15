using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context.Configuration;

namespace PredmetProjekat.Repositories.Context
{
    public class StoreContext : IdentityDbContext<Account>
    {
        public StoreContext(DbContextOptions options) : base(options) { }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Register> Registers { get; set; }
        public DbSet<Receipt> Receipts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Product>().Property(x => x.Quantity).HasDefaultValue(0);
            builder.Entity<Product>().Property(x => x.IsInStock).HasDefaultValue(false);
            builder.Entity<Register>().HasIndex(x => x.RegisterCode).IsUnique();
            builder.Entity<Account>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<Brand>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<Category>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);

            //TODO Seed the db with values?
            //One Admin

        }
    }
}
