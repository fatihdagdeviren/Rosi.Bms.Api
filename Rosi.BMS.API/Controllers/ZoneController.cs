using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Business.Constants;
using Rosi.BMS.API.Core.Utilities.Results;
using Rosi.BMS.API.Entities.Dtos.Zone;
using System.Data;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZoneController : ControllerBase
    {
        private IZoneService _zoneService;
        public ZoneController(IZoneService zoneService)
        {
            _zoneService = zoneService;
        }

        [HttpPost("Add")]       
        public async Task<ActionResult<ApiResult<object>>> Add(CreateZoneDto model)
        {
            var zoneEntity = await _zoneService.Add(model);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = zoneEntity,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpGet("Delete")]
        public async Task<ActionResult<ApiResult<bool>>> Delete([FromQuery]int id)
        {
            bool deleteResult = await _zoneService.Delete(id);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = deleteResult,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ApiResult<bool>>> Update([FromBody] UpdateZoneDto model)
        {
            await _zoneService.Update(model);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = true,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

    }
}
