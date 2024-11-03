using System;
using NoblePermission.Persistance.Entities;
using BC = BCrypt.Net.BCrypt;

namespace NoblePermission.Persistance.Extensions
{
    public static class ModelBuilder
    {
        public static void Seed(this Microsoft.EntityFrameworkCore.ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(new User
            {
                Id = Guid.Parse("5f8d5614-2c7e-4ec0-868c-d254e6516b4d"),
                UserName = "Permission",
                Email = "permission@gmail.com",
                Password = BC.HashPassword("Permission@123")
            });
            modelBuilder.Entity<WhiteLabelling>().HasData(new WhiteLabelling
            {
                Id = Guid.Parse("fd7803dc8eaa4a6e91dedca86dddabb0"),
                Heading = "ERP FOR SMALL & MEDIUM ENTERPRISE",
                Description = "Handle All your Needs & Automate Business Process",
                CompanyName = "TechoQode (Pvt) Ltd.",
                ApplicationName = "NOBLEPOS",
                AddressLine1 = "",
                AddressLine2 = "",
                AddressLine3 = "",
            }); ;
        }
    }
}
