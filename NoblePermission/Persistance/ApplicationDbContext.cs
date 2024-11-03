using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoblePermission.BusinessLayer;
using NoblePermission.Persistance.Entities;
using NoblePermission.Persistance.Extensions;
using ModelBuilder = Microsoft.EntityFrameworkCore.ModelBuilder;

namespace NoblePermission.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<NobleGroup> NobleGroups { get; set; }
        public DbSet<NobleModule> NobleModules { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Entities.NoblePermission> NoblePermissions { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyLicense> CompanyLicenses { get; set; }
        public DbSet<CompanyPermission> CompanyPermissions { get; set; }
        public DbSet<PaymentLimit> PaymentLimits { get; set; }
        public DbSet<WhiteLabelling> WhiteLabellings { get; set; }
        public DbSet<FtpAccountDetail> FtpAccountDetails { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
        }

       
    }
}
