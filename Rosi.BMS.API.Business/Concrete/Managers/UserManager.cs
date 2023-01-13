using System.Collections.Generic;
using Rosi.BMS.API.Business.Abstract;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.DataAccess.Abstract;

namespace Rosi.BMS.API.Business.Concrete.Managers
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user);
        }

        public User GetByMail(string email)
        {
            //TODO: buralar async
            return  _userDal.Get(u => u.Email == email).Result;
        }
    }
}