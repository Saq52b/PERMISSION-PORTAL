using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.ViewModel
{
    public class CompanyLookUp
    {
        public ICollection<CompanyLicenseLookUp> CompanyLicenseLookUps { get; set; }
        public ICollection<CompanyInfoLookUp> CompanyInfoLookUps { get; set; }
    }
}
