using AutoMapper;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;
using System.Threading.Tasks;
using System;
using Rosi.BMS.API.Entities.Dtos.Zone;
using System.Collections.Generic;

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
        public async Task<ZoneDto> Add(CreateZoneDto model)
        {
            var entity =  _mapper.Map<Zone>(model);       
            entity.IsActive = true;
            return _mapper.Map<ZoneDto>(await _zoneDal.Add(entity));            
        }

        public async Task<bool> Delete(int id)
        {
            return await _zoneDal.Delete(id);            
        }

        public async Task<List<ZoneDto>> GetAll()
        {
            var zoneList = await _zoneDal.GetList();
            var retList = new List<ZoneDto>();
            foreach (var zone in zoneList)
            {
                retList.Add(_mapper.Map<ZoneDto>(zone));
            }
            return retList;
        }

        public async Task<ZoneDto> GetById(int id)
        {
            return _mapper.Map<ZoneDto>(await _zoneDal.Get(x => x.Id == id));           
        }
      
        public async Task Update(UpdateZoneDto model)
        {
            var zone = _mapper.Map<Zone>(model);
            var oldObject = await _zoneDal.Get(x => x.Id == model.Id);           
            await _zoneDal.Update(zone);
        }

        public async Task<bool> TestMethod()
        {
            return await Task.FromResult(true);
        }

    }
}
