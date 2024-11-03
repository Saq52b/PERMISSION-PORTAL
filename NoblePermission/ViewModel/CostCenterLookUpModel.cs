using System;
using System.Collections.Generic;


namespace NoblePermission.ViewModel
{
    public class CostCenterLookUpModel
    {
        public Guid? Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string NameArabic { get; set; }
        public string Description { get; set; }
        public IList<AccountLookUpModel> Accounts { get; set; }
    }
}
