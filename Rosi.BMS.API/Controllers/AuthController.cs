using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Business.Constants;
using Rosi.BMS.API.Core.Utilities.Results;
using Rosi.BMS.API.Core.Utilities.Security.Jwt;
using Rosi.BMS.API.Entities.Dtos;
using System;
using System.Threading.Tasks;

namespace Rosi.BMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthService _authService;     
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]     
        public ActionResult Login(UserForLoginDto userForLoginDto)
        {         
            var userToLogin = _authService.Login(userForLoginDto);
            if (userToLogin == null)
            {
                return BadRequest(new ApiResult<object>
                {
                    Success = false,
                    Message = Messages.NullReturned,
                    InternalMessage = Messages.NullReturned,
                    Data = null
                });
            }

            var result = _authService.CreateAccessToken(userToLogin);
            if (result != null)
            {
                return Ok(new ApiResult<AccessToken>
                {
                    Success = true,
                    Message = Messages.Success,
                    InternalMessage = Messages.Success,
                    Data = result
                });
            }

            return BadRequest(new ApiResult<object>
            {
                Success = false,
                Data = null
            });
        }

        [HttpPost("register")]
        public ActionResult Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = _authService.UserExists(userForRegisterDto.Email);
            if (!userExists)
                return BadRequest(new ApiResult<object>
                {
                    Success = false,
                    Data = null,
                    Message = Messages.UserAlreadyExists,
                    InternalMessage = Messages.UserAlreadyExists,
                });

            var registerResult = _authService.Register(userForRegisterDto, userForRegisterDto.Password);
            var result = _authService.CreateAccessToken(registerResult);
            if (result != null)
            {
                return Ok(new ApiResult<AccessToken>
                {
                    Success = true,
                    Message = Messages.Success,
                    InternalMessage = Messages.Success,
                    Data = result
                });
            }

            return BadRequest(new ApiResult<object>
            {
                Success = false,
                Data = null
            });
        }
    }
}
