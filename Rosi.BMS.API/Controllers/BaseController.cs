using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;
using System.Linq;
using Rosi.BMS.API.Core.Entities;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Business.Constants;
using Rosi.BMS.API.Core.Utilities.Results;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace Rosi.BMS.API.Controllers
{
    [ApiController]
    public abstract class BaseController<TService, T, C, U> : ControllerBase
        where T : IDto
        where C : IDto
        where U : IDto
        where TService : IBaseService<T, C, U>                                       

    {
        public TService _service; 
        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResult<T>>> Add(C model)
        {
            var addResult = await _service.Add(model);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = addResult,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpDelete("Delete")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResult<bool>>> Delete([FromQuery] int id)
        {
            bool deleteResult = await _service.Delete(id);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = deleteResult,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpPut("Update")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResult<bool>>> Update([FromBody] U model)
        {
            await _service.Update(model);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = true,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpGet("GetAll")]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResult<T>>> GetAll()
        {
            var listResult = await _service.GetAll();
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = listResult,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

        [HttpGet("GetById")]
        //[Authorize(Roles = "User")]
        public async Task<ActionResult<ApiResult<T>>> GetById([FromQuery] int id)
        {
            var obj = await _service.GetById(id);
            return Ok(new ApiResult<object>
            {
                Success = true,
                Data = obj,
                Message = Messages.Success,
                InternalMessage = Messages.Success
            });
        }

    }
}
