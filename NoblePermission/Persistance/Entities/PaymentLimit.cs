using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.Persistance.Entities
{
    public class PaymentLimit
    {
        public Guid Id { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }
        public string Message { get; set; }
        public bool IsActive { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
