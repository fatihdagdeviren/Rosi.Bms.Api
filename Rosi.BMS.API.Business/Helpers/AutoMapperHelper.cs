using AutoMapper;
using Rosi.BMS.API.Entities.Concrete;
using Rosi.BMS.API.Entities.Dtos.Zone;

namespace Rosi.BMS.API.Business.Helpers
{
    public class AutoMapperHelper : Profile
    {
        public AutoMapperHelper()
        {
            CreateMap<CreateZoneDto, Zone>();
            CreateMap<UpdateZoneDto, Zone>();
        }
    }
}