using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.Persistance.Configuration
{
    public class PaymentLimitConfiguration:IEntityTypeConfiguration<PaymentLimit>
    {
        public void Configure(EntityTypeBuilder<PaymentLimit> builder)
        {
            builder.HasOne(ea => ea.Company)
                .WithMany(a => a.PaymentLimits)
                .HasForeignKey(ea => ea.CompanyId);
        }
    }
}
