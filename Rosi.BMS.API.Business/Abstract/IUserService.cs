using System.Collections.Generic;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);

        void Add(User user);

        User GetByMail(string email);
    }
}