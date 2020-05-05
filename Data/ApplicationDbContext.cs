using System;
using System.Collections.Generic;
using System.Text;
using DiabeticAide.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using DiabeticAide.Models.ViewModels;

namespace DiabeticAide.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<UserData> UserData { get; set; }
        public DbSet<UserHelper> UserHelpers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ApplicationUser user = new ApplicationUser
            {
                FirstName = "Maddie",
                LastName = "Fike",
                UserName = "sweetWilly3k@gmail.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                DoctorEmail = "william.g.metcalf@gmail.com",
                DoctorName = "Carrie",
                DoctorPhone = "918-123-1234",
                IsPatient = true
            };
            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin8*");
            modelBuilder.Entity<ApplicationUser>().HasData(user);
        }

        public DbSet<DiabeticAide.Models.ViewModels.UserViewModel> UserViewModel { get; set; }
    }
}