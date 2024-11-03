using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.ViewModel
{
    public class GetAllPermissionModuleAndGroup
    {

        public GroupLookUp GroupLookUp { get; set; }
        public WhiteLabelLookUp WhiteLabelLookUp { get; set; }
        public List<ModuleLookUp> ModuleLookUps { get; set; }
        public List<PermissionsLookUp> PermissionsLookUps { get; set; }
        public List<CompanyLicenseLookUp> CompanyLicenseLookUps { get; set; }
        public AccountTemplateDto TemplateLists { get; set; }
    }
}
