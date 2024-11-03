using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.ViewModel
{
    public class GetCompanyDataLookUp
    {
        public ICollection<CompanyInfoLookUp> Companies { get; set; }
        public ICollection<CompanyInfoLookUp> Businesses { get; set; }
        public ICollection<CompanyInfoLookUp> Locations { get; set; }
        
    }
}
