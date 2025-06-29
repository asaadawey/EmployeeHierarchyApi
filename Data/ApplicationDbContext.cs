﻿using EmployeeHierarchyApi.Data.Entities;
using EmployeeHierarchyApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EmployeeHierarchyApi.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Employee> Employees { get; set; }

    public DbSet<User> Users { get; set; }
    // Build the database and it's relationship
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var passwordHasher = new PasswordHasher<User>();

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
            entity.Property(e => e.Title).IsRequired().HasMaxLength(100);

            entity.HasOne(e => e.Manager)
                  .WithMany(e => e.Subordinates)
                  .HasForeignKey(e => e.ManagerId)
                  .OnDelete(DeleteBehavior.Restrict);

            // For caching
            entity.HasIndex(e => e.ManagerId);
        });

        // Seed data
        modelBuilder.Entity<Employee>().HasData(
            new Employee { Id = 1, Name = "John Smith", Title = "CEO", ManagerId = null },
            new Employee { Id = 2, Name = "Sarah Johnson", Title = "CTO", ManagerId = 1 },
            new Employee { Id = 3, Name = "Mike Wilson", Title = "VP Sales", ManagerId = 1 },
            new Employee { Id = 4, Name = "Lisa Brown", Title = "Engineering Manager", ManagerId = 2 },
            new Employee { Id = 5, Name = "Tom Davis", Title = "Senior Developer", ManagerId = 4 },
            new Employee { Id = 6, Name = "Anna Garcia", Title = "Developer", ManagerId = 4 },
            new Employee { Id = 7, Name = "David Miller", Title = "Sales Manager", ManagerId = 3 }
        );

        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Username = "testuser",
                Email = "test@example.com",
                PasswordHash = passwordHasher.HashPassword(new User(), "1234"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                IsActive = true
            },
            new User
            {
                Id = 2,
                FirstName = "Test",
                LastName = "Test",
                Username = "testuser2",
                Email = "test2@example.com",
                PasswordHash = passwordHasher.HashPassword(new User(), "1234"),
                Role = "Admin",
                CreatedAt = DateTime.UtcNow,
                LastLoginAt = null,
                IsActive = true
            }
        );
    }
}