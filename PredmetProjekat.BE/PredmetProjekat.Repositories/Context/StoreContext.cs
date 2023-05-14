﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Employee> Employees { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());

            builder.Entity<Brand>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Category>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Product>().HasIndex(x => x.Name).IsUnique();
            builder.Entity<Register>().HasIndex(x => x.RegisterCode).IsUnique();

            //modelBuilder.Entity<Employee>()
            //    .HasOne(x => x.ManagerUsername)
            //    .WithMany()
            //    .HasForeignKey("ManagerUsername")
            //    .OnDelete(DeleteBehavior.Restrict);

            //TODO Seed the db with values

        }
    }
}
