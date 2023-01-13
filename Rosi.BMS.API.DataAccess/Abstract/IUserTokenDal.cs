using System.Collections.Generic;
using Rosi.BMS.API.Core.DataAccess;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.DataAccess.Abstract
{
    public interface IUserTokenDal : IEntityRepository<UserToken>
    {
    }
}