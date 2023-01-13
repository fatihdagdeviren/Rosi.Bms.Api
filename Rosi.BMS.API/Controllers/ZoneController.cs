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
    public class ZoneController : BaseController<IZoneService, ZoneDto, CreateZoneDto, UpdateZoneDto>
    {
        public ZoneController(IZoneService service) : base(service)
        {
            
        }

        [HttpGet("TestMethod")]
        public async Task<ActionResult<ApiResult<bool>>> TestMethod()
        {            
            var result = await _service.TestMethod();
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = result,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        #region Eski-ZoneController
        //private IZoneService _zoneService;


        //public ZoneController(IZoneService zoneService)
        //{
        //    _zoneService = zoneService;
        //}

        //[HttpPost("Add")]       
        //public async Task<ActionResult<ApiResult<ZoneDto>>> Add(CreateZoneDto model)
        //{
        //    var zoneEntity = await _zoneService.Add(model);
        //    return Ok(new ApiResult<object>
        //    {
        //        Success = true,
        //        Data = zoneEntity,
        //        Message = Messages.Success,
        //        InternalMessage = Messages.Success
        //    });
        //}

        //[HttpGet("Delete")]
        //public async Task<ActionResult<ApiResult<bool>>> Delete([FromQuery]int id)
        //{
        //    bool deleteResult = await _zoneService.Delete(id);
        //    return Ok(new ApiResult<object>
        //    {
        //        Success = true,
        //        Data = deleteResult,
        //        Message = Messages.Success,
        //        InternalMessage = Messages.Success
        //    });
        //}

        //[HttpPut("Update")]
        //public async Task<ActionResult<ApiResult<bool>>> Update([FromBody] UpdateZoneDto model)
        //{
        //    await _zoneService.Update(model);
        //    return Ok(new ApiResult<object>
        //    {
        //        Success = true,
        //        Data = true,
        //        Message = Messages.Success,
        //        InternalMessage = Messages.Success
        //    });
        //}

        //[HttpGet("GetAll")]
        //public async Task<ActionResult<ApiResult<ZoneDto>>> GetAll()
        //{
        //    var zoneList = await _zoneService.GetAll();
        //    return Ok(new ApiResult<object>
        //    {
        //        Success = true,
        //        Data = zoneList,
        //        Message = Messages.Success,
        //        InternalMessage = Messages.Success
        //    });
        //}

        //[HttpGet("GetById")]
        //public async Task<ActionResult<ApiResult<ZoneDto>>> GetById([FromQuery] int id)
        //{
        //    var zone = await _zoneService.GetById(id);
        //    return Ok(new ApiResult<object>
        //    {
        //        Success = true,
        //        Data = zone,
        //        Message = Messages.Success,
        //        InternalMessage = Messages.Success
        //    });
        //}
        #endregion

    }
}
