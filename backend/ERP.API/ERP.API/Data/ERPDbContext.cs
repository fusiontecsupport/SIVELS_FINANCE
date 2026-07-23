using Microsoft.EntityFrameworkCore;
using ERP.API.Models;

namespace ERP.API.Data
{
    public class ERPDbContext : DbContext
    {
        public ERPDbContext(DbContextOptions<ERPDbContext> options)
            : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<AspNetRole> AspNetRoles { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }
        public DbSet<CustomerMaster> CustomerMasters { get; set; }
        public DbSet<CustomerLocationDetail> CustomerLocationDetails { get; set; }

        public DbSet<EmailOtp> EmailOtps { get; set; }

        public DbSet<CompanyMaster> CompanyMasters { get; set; }
        public DbSet<StateMaster> StateMasters { get; set; }
        public DbSet<LocationMaster> LocationMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ApplicationRoleGroup>()
                .HasKey(rg => new { rg.RoleId, rg.GroupId });

            modelBuilder.Entity<ApplicationUserGroup>()
                .HasKey(ug => new { ug.UserId, ug.GroupId });

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.MobileNumber)
                .IsUnique();
        }
    }
}