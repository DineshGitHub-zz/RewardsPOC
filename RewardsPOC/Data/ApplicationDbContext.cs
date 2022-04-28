using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RewardsPOC.Entity;
using RewardsPOC.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RewardsPOC.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("hl");
            builder.Entity<ApplicationUser>()
                .HasMany(x => x.Children)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ManagerId)
            .OnDelete(DeleteBehavior.ClientSetNull);
            builder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<int>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }

        public DbSet<RewardTypes> RewardTypes { get; set; }

        public DbSet<Rewards> Rewards { get; set; }
    }
}
