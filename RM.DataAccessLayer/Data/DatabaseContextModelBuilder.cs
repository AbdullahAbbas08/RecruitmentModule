using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RM.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RM.DataAccessLayer.Data
{
    public static class DatabaseContextModelBuilder
    {
        public static void Build(ModelBuilder modelBuilder)
        {
            // Change Identity Table Name
            modelBuilder.Entity<AppRole>().ToTable("Roles", "Security");
            modelBuilder.Entity<AppUserRole>().ToTable("UserRoles", "Security");
            modelBuilder.Entity<AppUser>().ToTable("Users", "Security");

            modelBuilder.Entity<IdentityUser>()
                   .Ignore(e => e.Email)
                   .Ignore(e => e.EmailConfirmed);


            modelBuilder.Entity<AppUser>(b =>
            {
                // Primary key
                b.HasKey(u => u.Id);

                // Indexes for "normalized" username , to allow efficient lookups
                b.HasIndex(u => u.NormalizedUserName).HasDatabaseName("UserNameIndex").IsUnique();

                // Maps to the Users table
                b.ToTable("Users");

                // Limit the size of columns to use efficient database types
                b.Property(u => u.UserName).HasMaxLength(256);
                b.Property(u => u.NormalizedUserName).HasMaxLength(256);


                b.HasOne(c => c.Title).WithMany(p => p.Users).HasForeignKey(c => c.TitleId).OnDelete(DeleteBehavior.Restrict).IsRequired();              

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.User)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });


            modelBuilder.Entity<AppRole>(b =>
            {
                // Primary key
                b.HasKey(r => r.Id);

                // Index for "normalized" role name to allow efficient lookups
                b.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();

                // Maps to the AspNeAppRoles table
                b.ToTable("Roles");

                // Limit the size of columns to use efficient database types
                b.Property(u => u.Name).HasMaxLength(256);
                b.Property(u => u.NormalizedName).HasMaxLength(256);

                // Each Role can have many entries in the UserRole join table
                b.HasMany(e => e.UserRoles)
                    .WithOne(e => e.Role)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
           
            });

            modelBuilder.Entity<AppUserRole>(b =>
            {
                // Primary key
                b.HasKey(r => new { r.UserId, r.RoleId });

                // Maps to the AspNeAppUserRoles table
                b.ToTable("UserRoles");
            });

        }
    }
}
