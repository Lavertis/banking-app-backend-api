﻿using BankApp.Entities.UserTypes;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BankApp.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Admin> Admins { get; set; } = default!;
    public DbSet<Employee> Employees { get; set; } = default!;
    public DbSet<Customer> Customers { get; set; } = default!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseLazyLoadingProxies();
    }

    protected override async void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Only needed for login via 3rd party account
        builder.Entity<IdentityUserToken<string>>().Metadata.SetIsTableExcludedFromMigrations(true);
        builder.Entity<IdentityUserLogin<string>>().Metadata.SetIsTableExcludedFromMigrations(true);

        // Create relationships
        builder.Entity<Admin>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Employee>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Customer>()
            .HasOne(e => e.AppUser)
            .WithOne()
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        // // Create roles
        // const string adminRoleId = "3dff7514-ee17-42d1-af59-7255d674a3e8";
        // const string adminAppUserId = "a380ad98-8597-4bd5-836e-831110e07951";
        // const string adminId = "b6beef19-096f-4cbd-b470-266eae6f5c72";
        // builder.Entity<IdentityRole>().HasData(
        //     new IdentityRole {Name = "Admin", NormalizedName = "ADMIN", Id = adminRoleId},
        //     new IdentityRole {Name = "Employee", NormalizedName = "EMPLOYEE"},
        //     new IdentityRole {Name = "Customer", NormalizedName = "CUSTOMER"}
        // );
        //
        // // Create Admin account
        // var hasher = new PasswordHasher<AppUser>();
        // builder.Entity<AppUser>().HasData(
        //     new AppUser
        //     {
        //         Id = adminAppUserId,
        //         UserName = "admin",
        //         NormalizedUserName = "admin".Normalize().ToUpperInvariant(),
        //         PasswordHash = hasher.HashPassword(null!, "admin")
        //     }
        // );
        // builder.Entity<IdentityUserRole<string>>().HasData(
        //     new IdentityUserRole<string>
        //     {
        //         RoleId = adminRoleId,
        //         UserId = adminAppUserId
        //     }
        // );
        // builder.Entity<Admin>().HasData(
        //     new Admin
        //     {
        //         Id = adminId,
        //         AppUserId = adminAppUserId
        //     }
        // );
    }
}