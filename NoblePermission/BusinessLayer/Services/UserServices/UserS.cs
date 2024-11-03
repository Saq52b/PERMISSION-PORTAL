using System;
using System.Linq;
using System.Text.RegularExpressions;
using NoblePermission.Persistance;
using BC = BCrypt.Net.BCrypt;

namespace NoblePermission.BusinessLayer.Services.UserServices
{
    public class UserS:IUser
    {
        private readonly ApplicationDbContext _context;

        public UserS(ApplicationDbContext context)
        {
            _context = context;
        }
        public object LoginUser(string email, string password)
        {
            try
            {
                //Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
                //if (!emailRegex.IsMatch(email))
                //    return "Please Enter valid email";

                var data = _context.Users.FirstOrDefault(a => a.Email == email || a.UserName == email);

                if (data == null)
                    return "Please register your email";
                if (!BC.Verify(password, data.Password))
                    return "Please enter correct password";

                return data;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
