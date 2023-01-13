using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.Entities.Concrete;
using Rosi.BMS.API.Entities.Dtos.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Business.Abstract
{
    public interface IZoneService
    {
        Task<Zone> Add(CreateZoneDto zone);
        Task<bool> Delete(int id);
        Task Update(UpdateZoneDto zone);
    }
}
