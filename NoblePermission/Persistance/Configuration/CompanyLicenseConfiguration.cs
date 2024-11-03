using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.Persistance.Configuration
{
    public class CompanyLicenseConfiguration : IEntityTypeConfiguration<CompanyLicense>
    {
       
        public void Configure(EntityTypeBuilder<CompanyLicense> builder)
        {
            builder.HasOne(ea => ea.Company)
                .WithMany(a => a.CompanyLicenses)
                .HasForeignKey(ea => ea.CompanyId);
        }
    }
}
