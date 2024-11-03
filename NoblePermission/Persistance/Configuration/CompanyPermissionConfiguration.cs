using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.Persistance.Configuration
{
    public class CompanyPermissionConfiguration:IEntityTypeConfiguration<CompanyPermission>
    {
        public void Configure(EntityTypeBuilder<CompanyPermission> builder)
        {
            builder.HasOne(a => a.NobleModules)
                .WithMany(j => j.CompanyPermissions)
                .HasForeignKey(a => a.NobleModuleId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(a => a.NobleGroup)
                .WithMany(j => j.CompanyPermissions)
                .HasForeignKey(a => a.NobleGroupId);
            builder.HasOne(a => a.Company)
                .WithMany(j => j.CompanyPermissions)
                .HasForeignKey(a => a.CompanyId);
        }
    }
}
