using System.Collections.Generic;
using System.Threading.Tasks;
using Rosi.BMS.API.Core.DataAccess;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.DataAccess.Abstract
{
    public interface IUserDal : IEntityRepository<User>
    {
        List<OperationClaim> GetClaims(User user);
        Task<bool> SetUserOperationClaimDefault(User user);
    }
}