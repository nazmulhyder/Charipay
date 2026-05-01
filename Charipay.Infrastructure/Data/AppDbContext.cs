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
        public DbSet<Charity> Charities { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<VolunteerTask> VolunteerTasks { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<VolunteerUser> VolunteerUsers { get; set; }
        public DbSet<VolunteerProgressUpdate> VolunteerProgressUpdates { get; set; }
        public DbSet<UserFeedback> UserFeedbacks { get; set; }

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

            // charity
            modelBuilder.Entity<Charity>()
                .HasOne(c=>c.CreatedByUser)
                .WithMany(c=>c.Charities)
                .HasForeignKey(c => c.CreatedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

            //campaign
            modelBuilder.Entity<Campaign>()
                .HasOne(c=>c.Charity)
                .WithMany(c=>c.Campaigns)
                .HasForeignKey(c => c.CharityId)
                .OnDelete(DeleteBehavior.Cascade);

            //donation
            modelBuilder.Entity<Donation>()
                .HasOne(c => c.User)
                .WithMany(c => c.Donations)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Donation>()
             .HasOne(c => c.Campaign)
             .WithMany(c => c.Donations)
             .HasForeignKey(c => c.CampaignId);

            //Volunteer Task
            modelBuilder.Entity<VolunteerTask>()
                .HasOne(c => c.Campaign)
                .WithMany(c => c.VolunteerTasks)
                .HasForeignKey(c => c.CampaignId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<VolunteerTask>()
                .HasMany(c=>c.VolunteerUsers)
                .WithOne(d => d.VolunteerTask)
                .HasForeignKey(d=>d.VolunteerTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            //Volunteer User
            modelBuilder.Entity<VolunteerUser>()
                .HasOne(c => c.VolunteerTask)
                .WithMany(c => c.VolunteerUsers)
                .HasForeignKey(c => c.VolunteerTaskId);


            modelBuilder.Entity<VolunteerUser>()
                .HasOne(c => c.User)
                .WithMany(c => c.VolunteerUsers)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
                 

            modelBuilder.Entity<VolunteerUser>()
                .HasMany(c=>c.volunteerProgressUpdates)
                .WithOne(d=>d.VolunteerUser)
                .HasForeignKey(d=>d.VolunteerUserId)
                .OnDelete(DeleteBehavior.Cascade);





            modelBuilder.Entity<UserFeedback>(entity =>
            {
                entity.HasKey(f => f.UserFeedbackId);

                entity.Property(x => x.UserId)
         .IsRequired(false);

                entity.Property(f => f.Message)
                      .IsRequired()
                      .HasMaxLength(1000);

                entity.Property(f => f.PageUrl)
                      .HasMaxLength(300);

                entity.Property(f => f.AdminNote)
                      .HasMaxLength(500);

                entity.Property(f => f.Rating)
                      .IsRequired(false);

                entity.Property(f => f.CreatedAt)
                      .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(f => f.FeedbackType)
                      .HasConversion<string>();

                entity.Property(f => f.Status)
                      .HasConversion<string>();
            });

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
