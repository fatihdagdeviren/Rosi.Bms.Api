using System;
using System.Collections.Generic;
using System.Text;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.Business.Abstract
{
    public interface IUserTokenService
    {
        void Add(UserToken userToken);
    }
}
