using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.Persistance.Entities
{
    public class CompanyPermission
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public Guid NobleGroupId { get; set; }
        public virtual NobleGroup NobleGroup { get; set; }
        public virtual NobleModule NobleModules { get; set; }
        public Guid NobleModuleId { get; set; }
        public virtual Company Company { get; set; }
        public Guid CompanyId { get; set; }
    }
}
