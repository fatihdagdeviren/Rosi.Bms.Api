using System.Collections.Generic;
using System.Linq;
using Rosi.BMS.API.Core.DataAccess.EntityFramework;
using Rosi.BMS.API.Core.Entities.Concrete;
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
    }
}