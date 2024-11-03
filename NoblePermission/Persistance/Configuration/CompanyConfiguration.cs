using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.Persistance.Configuration
{
    public class CompanyConfiguration:IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasOne(ea => ea.NobleGroup)
                .WithMany(a => a.Companies)
                .HasForeignKey(ea => ea.NobleGroupId);
        }
    }
}
