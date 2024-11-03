using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.BusinessLayer;

namespace NoblePermission.Persistance.Entities
{
    public class NobleGroup
    {
        public Guid Id { get; set; }
        public String GroupName { get; set; }
        public GroupType GroupType { get; set; }
        
        public virtual ICollection<NoblePermission> NoblePermissions { get; set; }
        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<CompanyPermission> CompanyPermissions { get; set; }
    }
}
