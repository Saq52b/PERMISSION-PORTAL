using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.Persistance.Entities;

namespace NoblePermission.BusinessLayer.Services.PaymentLimitServices
{
    public interface IPaymentLimit
    {
        object GetLastPaymentLimit(Guid companyId);
        string AddUpdatePaymentLimit(PaymentLimit paymentLimit);
        
    }
}
