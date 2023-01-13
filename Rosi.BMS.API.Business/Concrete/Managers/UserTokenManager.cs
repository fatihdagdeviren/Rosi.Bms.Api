using System;
using System.Collections.Generic;
using System.Text;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;

namespace Rosi.BMS.API.Business.Concrete.Managers
{
    public class UserTokenManager: IUserTokenService
    {
        private readonly IUserTokenDal _tokenDal;

        public UserTokenManager(IUserTokenDal tokenDal)
        {
            _tokenDal = tokenDal;
        }

        public void Add(UserToken userToken)
        {
            _tokenDal.Add(userToken);
        }
    }
}
