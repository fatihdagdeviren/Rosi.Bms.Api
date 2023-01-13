using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Rosi.BMS.API.Core.DataAccess.EntityFramework;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;

namespace Rosi.BMS.API.DataAccess.Concrete
{
    public class EfUserTokenDal : EfEntityRepositoryBase<UserToken, RosiBMSApiDbContext>, IUserTokenDal
    {
    }
}
