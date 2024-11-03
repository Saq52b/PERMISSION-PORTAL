using NoblePermission.Persistance.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NoblePermission.Persistance;

namespace NoblePermission.BusinessLayer.Services.PaymentLimitServices
{
    public class PaymentLimitS : IPaymentLimit
    {
        public readonly ApplicationDbContext Context;
        public PaymentLimitS(ApplicationDbContext context)
        {
            Context = context;
        } 
        public string AddUpdatePaymentLimit(PaymentLimit paymentLimit)
        {
            try
            {
                if (paymentLimit.Id != Guid.Empty)
                {
                    var limit = Context.PaymentLimits.FirstOrDefault(x => x.Id == paymentLimit.Id);
                    if (limit == null)
                        return "This Payment Limit not exist";
                    limit.FromDate = paymentLimit.FromDate;
                    limit.ToDate = paymentLimit.ToDate;
                    limit.Message = paymentLimit.Message;
                    limit.IsActive = paymentLimit.IsActive;
                    limit.CompanyId = paymentLimit.CompanyId;

                    Context.SaveChanges();
                    return "Update";
                }
                else
                {
                    var limit = new PaymentLimit()
                    {
                        FromDate = paymentLimit.FromDate,
                        ToDate = paymentLimit.ToDate,
                        Message = paymentLimit.Message,
                        IsActive = paymentLimit.IsActive,
                        CompanyId = paymentLimit.CompanyId,
                    };

                    Context.PaymentLimits.Add(limit);
                    Context.SaveChanges();

                    return "Add";
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message;
            }
        }

        public object GetLastPaymentLimit(Guid companyId)
        {
            try
            {
                var lastActiveLimit = Context.PaymentLimits.ToList().LastOrDefault(x => x.IsActive && x.CompanyId == companyId);
                if (lastActiveLimit == null)
                    return new PaymentLimit();
                return lastActiveLimit;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return e.Message;
            }
        }
    }
}
