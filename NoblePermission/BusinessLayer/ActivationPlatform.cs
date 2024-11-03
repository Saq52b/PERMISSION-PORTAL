using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NoblePermission.BusinessLayer
{
    public enum ActivationPlatform
    {
        [Display(Name = "Offline")]
        Offline = 1,
        [Display(Name = "Online")]
        Online = 2,
        [Display(Name = "Both")]
        Both = 3
    }
}
