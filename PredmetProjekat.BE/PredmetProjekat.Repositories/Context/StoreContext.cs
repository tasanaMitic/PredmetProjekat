using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PredmetProjekat.Models.Models;
using PredmetProjekat.Repositories.Context.Configuration;
using System.Security.Cryptography;

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
        public DbSet<SoldProduct> SoldProducts { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductAttribute> Attributes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<Product>().Property(x => x.Price).HasDefaultValue(0);
            builder.Entity<Product>().Property(x => x.IsInStock).HasDefaultValue(false);
            builder.Entity<Product>().Property(x => x.IsDeleted).HasDefaultValue(false);

            builder.Entity<Category>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<Category>().HasIndex(x => x.Name).IsUnique();

            builder.Entity<ProductType>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<ProductType>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<ProductType>().HasMany(x => x.Attributes);

            builder.Entity<Brand>().Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();

            builder.Entity<Register>().HasIndex(x => x.RegisterCode).IsUnique();
            builder.Entity<Account>().Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}
