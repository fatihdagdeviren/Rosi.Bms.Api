using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.Core.Utilities.Security.Jwt;
using Rosi.BMS.API.Entities.Dtos;

namespace Rosi.BMS.API.Business.Abstract
{
    public interface IAuthService
    {
        User Register(UserForRegisterDto userForRegisterDto, string password);

        User Login(UserForLoginDto userForLoginDto);

        bool UserExists(string email);

        AccessToken CreateAccessToken(User user);
    }
}