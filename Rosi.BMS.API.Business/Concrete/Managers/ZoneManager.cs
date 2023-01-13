using AutoMapper;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;
using System.Threading.Tasks;
using System;
using Rosi.BMS.API.Entities.Dtos.Zone;

namespace Rosi.BMS.API.Business.Concrete.Managers
{
    public class ZoneManager : IZoneService
    {
        private readonly IZoneDal _zoneDal;
        private readonly IMapper _mapper;

        public ZoneManager(IZoneDal zoneDal, IMapper mapper)
        {
            _zoneDal = zoneDal;
            _mapper = mapper;
        }
        public async Task<Zone> Add(CreateZoneDto model)
        {
            var entity =  _mapper.Map<Zone>(model);
            entity.CreatedDate = DateTime.Now;
            entity.UpdatedDate = DateTime.Now;
            entity.IsActive = true;
            return await _zoneDal.Add(entity);            
        }

        public async Task<bool> Delete(int id)
        {
            return await _zoneDal.Delete(id);            
        }

        public async Task Update(UpdateZoneDto model)
        {
            var zone = _mapper.Map<Zone>(model);
            var oldObject = await _zoneDal.Get(x => x.Id == model.Id);
            zone.UpdatedDate = DateTime.Now;
            zone.CreatedDate = oldObject.CreatedDate;
            await _zoneDal.Update(zone);
        }
    }
}
