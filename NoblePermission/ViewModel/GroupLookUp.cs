using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.BusinessLayer;

namespace NoblePermission.ViewModel
{
    public class GroupLookUp
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string GroupType { get; set; }
    }
}
