using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.BusinessLayer;

namespace NoblePermission.Persistance.Entities
{
    public class CompanyLicense
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public bool IsBlock { get; set; }
        public bool IsActive { get; set; }
        public bool IsTechnicalSupport { get; set; }
        public bool IsUpdateTechnicalSupport { get; set; }
        public string TechnicalSupportPeriod { get; set; }
        public string PaymentFrequency { get; set; }
        public bool GracePeriod { get; set; }
        public string LicenseType { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
        public ActivationPlatform ActivationPlatform { get; set; }
    }
}
