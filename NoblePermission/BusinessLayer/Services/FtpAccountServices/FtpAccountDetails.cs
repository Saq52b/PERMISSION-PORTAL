using NoblePermission.Persistance;
using NoblePermission.Persistance.Entities;
using NoblePermission.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.BusinessLayer.Services.FtpAccountServices
{
    public class FtpAccountDetails : IFtpAccountDetail
    {
        public readonly ApplicationDbContext _context;

        public FtpAccountDetails(ApplicationDbContext context)
        {
            _context = context;
        }
        public string AddUpdateFtpDetail(FtpAccountDetailLookup ftpAccountDetail)
        {
            try
            {
                if (ftpAccountDetail.Id != Guid.Empty)
                {
                    var data = _context.FtpAccountDetails.Find(ftpAccountDetail.Id);
                    if (data == null)
                        throw new ApplicationException("This Ftp account not find");

                    data.Host = ftpAccountDetail.Host;
                    data.Username = ftpAccountDetail.Username;
                    data.Password = ftpAccountDetail.Password;
                    data.Port = ftpAccountDetail.Port;
                    data.SystemType = ftpAccountDetail.SystemType;

                    _context.SaveChanges();
                    return "Updated";
                }
                else
                {
                    var data = new FtpAccountDetail()
                    {
                        Host = ftpAccountDetail.Host,
                        Username = ftpAccountDetail.Username,
                        Password = ftpAccountDetail.Password,
                        Port = ftpAccountDetail.Port,
                        SystemType = ftpAccountDetail.SystemType,
                        CompanyId = ftpAccountDetail.CompanyId,
                    };
                    _context.FtpAccountDetails.Add(data);
                    _context.SaveChanges();
                    return "Added";
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        public object GetFtpAccountDetailsList(Guid companyId)
        {
            try
            {
                if (companyId == Guid.Empty || companyId == null)
                    throw new ApplicationException("Location id not empty");

                var record = _context.FtpAccountDetails.Where(x => x.CompanyId == companyId).ToList();
                var ftpAccountList = new List<FtpAccountDetailLookup>();
                foreach (var data in record)
                {
                    ftpAccountList.Add(new FtpAccountDetailLookup
                    {
                        Id = data.Id,
                        Host = data.Host,
                        Username = data.Username,
                        Password = data.Password,
                        Port = data.Port,
                        SystemType = data.SystemType,
                        CompanyId = data.CompanyId,
                    });
                }
                return ftpAccountList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }
    }
}
