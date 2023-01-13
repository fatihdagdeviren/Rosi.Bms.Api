using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Rosi.BMS.API.Core.DataAccess.EntityFramework;
using Rosi.BMS.API.Core.Entities.Concrete;
using Rosi.BMS.API.Core.Enums;
using Rosi.BMS.API.DataAccess.Abstract;
using Rosi.BMS.API.DataAccess.Concrete.EntityFramework.Context;

namespace Rosi.BMS.API.DataAccess.Concrete.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, RosiBMSApiDbContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user)
        {
            using (var context = new RosiBMSApiDbContext())
            {
                var result = from uoc in context.UserOperationClaims
                             join oc in context.OperationClaims
                                 on uoc.Id equals oc.Id
                             where uoc.UserId == user.Id
                             select new OperationClaim { Id = oc.Id, Name = oc.Name };

                return result.ToList();
            }               
        }

        public async Task<bool> SetUserOperationClaimDefault(User user)
        {
            using (var context = new RosiBMSApiDbContext())
            {
                UserOperationClaim opClaim = new UserOperationClaim { OperationClaimId = (int)EUser.User, UserId = user.Id };
                await context.UserOperationClaims.AddAsync(opClaim);
                await context.SaveChangesAsync();
                return true;
            }
        }
    }
}