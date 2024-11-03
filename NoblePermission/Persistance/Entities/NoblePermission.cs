using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.Persistance.Entities
{
    public class NoblePermission
    {
        public Guid Id { get; set; }
        public string PermissionName { get; set; }
        public string Key { get; set; }
        public string Value { get; set; } 
        public Guid NobleGroupId { get; set; }
        public NobleGroup NobleGroup { get; set; }
        public Guid NobleModuleId { get; set; }
        public NobleModule NobleModule { get; set; }
    }
}
