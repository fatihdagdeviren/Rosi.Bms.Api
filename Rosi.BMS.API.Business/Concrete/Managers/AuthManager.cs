using System;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.Core.Utilities.Security.Hashing;
using Rosi.BMS.API.Core.Utilities.Security.Jwt; 
using Rosi.BMS.API.Entities.Dtos;

namespace Rosi.BMS.API.Business.Concrete.Managers
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        private IUserTokenService _userTokenService;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper, IUserTokenService  userTokenService)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
            _userTokenService = userTokenService;
        }

        public User Register(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
            var user = new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                CreatedDate = DateTime.Now
            };
            _userService.Add(user);
            return user;
        }

        public User Login(UserForLoginDto userForLoginDto)
        {
            var userToCheck = _userService.GetByMail(userForLoginDto.Email);
            if (userToCheck == null)
            {
                throw new Exception("hata metni");
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                throw new Exception("hata metni"); ;
            }

            return userToCheck;
        }

        public bool UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return false;
            }
            return true;
        }

        public AccessToken CreateAccessToken(User user)
        {
            var claims = _userService.GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims);
            _userTokenService.Add(new UserToken()
            {
                CreatedDate = DateTime.Now,
                Expiration = accessToken.Expiration,
                Token = accessToken.Token,
                User = user
            });
            return accessToken;
        }
    }
}