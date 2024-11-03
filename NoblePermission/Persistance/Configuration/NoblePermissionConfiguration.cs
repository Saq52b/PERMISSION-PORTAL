using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.Persistance.Configuration
{
    public class NoblePermissionConfiguration:IEntityTypeConfiguration<Entities.NoblePermission>
    {
        public void Configure(EntityTypeBuilder<Entities.NoblePermission> builder)
        {
            builder.HasOne(x => x.NobleGroup)
                .WithMany(x => x.NoblePermissions)
                .HasForeignKey(x => x.NobleGroupId);
            builder.HasOne(x => x.NobleModule)
                .WithMany(x => x.NoblePermissions)
                .HasForeignKey(x => x.NobleModuleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
