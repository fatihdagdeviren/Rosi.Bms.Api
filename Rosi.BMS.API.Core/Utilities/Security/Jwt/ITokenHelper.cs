using System.Collections.Generic;
using Rosi.BMS.API.Core.Entities.Concrete;

namespace Rosi.BMS.API.Core.Utilities.Security.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}