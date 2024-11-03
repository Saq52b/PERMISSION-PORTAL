using NoblePermission.Persistance.Entities;
using NoblePermission.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.BusinessLayer.Services.FtpAccountServices
{
    public interface IFtpAccountDetail
    {
        object GetFtpAccountDetailsList(Guid companyId);
        string AddUpdateFtpDetail (FtpAccountDetailLookup ftpAccountDetail);
    }
}
