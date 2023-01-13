using Rosi.BMS.API.Core.DataAccess.EntityFramework;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;
using Rosi.BMS.API.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosi.BMS.API.DataAccess.Concrete.EntityFramework
{
    public class EfZoneDal : EfEntityRepositoryBase<Zone, RosiBMSApiDbContext>, IZoneDal
    {
    }
}
