using System;
using System.Collections.Generic;

namespace NoblePermission.Persistance.Entities
{
    public class NobleModule
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<NoblePermission> NoblePermissions { get; set; }
        public virtual ICollection<CompanyPermission> CompanyPermissions { get; set; }
    }
}
