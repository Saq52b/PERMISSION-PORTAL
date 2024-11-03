using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.BusinessLayer.Services.UserServices
{
    public interface IUser
    {
        public object LoginUser(string email, string password);
    }
}
