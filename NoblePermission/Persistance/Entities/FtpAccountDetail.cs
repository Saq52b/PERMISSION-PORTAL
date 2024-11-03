using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.Persistance.Entities
{
    public class FtpAccountDetail
    {
        public Guid Id { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SystemType { get; set; }
        public Guid CompanyId { get; set; }
        public virtual Company Company { get; set; }
    }
}
