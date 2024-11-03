using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.ViewModel
{
    public class NoblePermissionLookUp
    {
        public Guid GroupId { get; set; }
        public Guid? CompanyId { get; set; }
        public ICollection<NobleModule> Modules { get; set; }
        public ICollection<PermissionsLookUp> Permissions { get; set; }
    }
}
