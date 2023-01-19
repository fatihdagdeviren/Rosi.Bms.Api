using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Rosi.BMS.API.Core.Entities.Concrete
{
    public class UserToken : BaseEntity
    {   
        [ForeignKey("Users")]
        public int UserId { get; set; }
        public virtual User  User{ get; set; }
        public string Token { get; set; }  // Token Değeri
        public DateTime Expiration { get; set; } // Token geçerlilik süresi

    }
}
