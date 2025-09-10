using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Emit;
using Charipay.Domain.Entities;

namespace Charipay.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // DbSets for your entities
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            #region Composite key for join table

            modelBuilder.Entity<UserRole>()
                .HasKey(u => new { u.UserID, u.RoleID });

            modelBuilder.Entity<UserRole>()
                .HasOne(u => u.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(u => u.UserID);

            modelBuilder.Entity<UserRole>()
              .HasOne(u => u.Role)
              .WithMany(u => u.UserRoles)
              .HasForeignKey(u => u.RoleID);

            #endregion

            #region Unique index on email
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
            #endregion

            #region Seed default roles (only IDs consistend if you want fixed ids)
            modelBuilder.Entity<Role>().HasData(
                  new Role { RoleID = 1, Name = "Admin"},
                  new Role { RoleID = 2, Name = "Donor"},
                  new Role { RoleID = 3, Name = "Volunteer" }
                );
            #endregion
        }

    }
}
