using Rosi.BMS.API.Core.Entities;

namespace Rosi.BMS.API.Entities.Dtos
{
    public class UserForLoginDto : IDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}