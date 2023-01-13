using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Rosi.BMS.API.Core.Entities.Concrete
{
    public class OperationClaim : BaseEntity
    { 
        public string Name { get; set; }
    }
}