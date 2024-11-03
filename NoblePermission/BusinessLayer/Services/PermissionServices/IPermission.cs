using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.Persistance.Entities;
using NoblePermission.ViewModel;

namespace NoblePermission.BusinessLayer.Services.PermissionServices
{
    public interface IPermission
    {
        GetAllPermissionModuleAndGroup GetAllPermissionDataAsync(Guid id, string systemType);
        WhiteLabelLookUp GetWhiteLabelingData();
        List<Persistance.Entities.NoblePermission> GetPermissionByGroupId(Guid id);
        List<NobleModule> GetAllNobleModule();
        object GetNobleGroupById(Guid id);
        string AddPermission(NoblePermissionLookUp noblePermissionLook);
        string SaveCompanyPermissions(NoblePermissionLookUp noblePermissionLook);
        object AddCompanyInformation(CompanyLookUp companyLookUp);
        object GetCompanyList(string searchTerm, int? pageNumber);
        object CompanyLicensing(CompanyLicenseLookUp companyLicenseLookUp);
        object GetLicenseDetail(Guid companyId);
        object GetCompanyPermissionById(Guid id);
        object DisActiveLicense(Guid companyId, bool isCompany);
    }
}
