using System;
using System.Collections.Generic;

namespace NoblePermission.ViewModel
{
    public class AccountTypeLookUp
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public IList<CostCenterLookUpModel> CostCenters { get; set; }
    }
}
