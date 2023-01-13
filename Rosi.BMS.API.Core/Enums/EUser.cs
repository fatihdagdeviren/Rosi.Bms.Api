using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Rosi.BMS.API.Core.Enums
{
    public enum EUser
    {
        [Display(Name = "User")]
        User = 1,

        [Display(Name = "Admin")]
        Admin = 99
    }
}
