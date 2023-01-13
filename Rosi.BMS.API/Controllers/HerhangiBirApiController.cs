using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Rosi.BMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HerhangiBirApiController : ControllerBase
    {
        /// <summary>
        /// Bu metodu çağırabilmek için  Authorize işlemi gerekmektedir.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetList")]
        //[Authorize]
        [Authorize(Roles = "Admin, User")]
        public IEnumerable<string> Get()
        {
            var rolesClaims = HttpContext.User.Claims.Where(p => p.Type.Contains("role"));
            var principal = HttpContext.User;
            if (principal?.Claims != null)
            {
                foreach (var claim in principal.Claims)
                {

                }
            }
            return new string[] { "fatih", "asdasd", "dfsdfd" };
        }


        /// <summary>
        /// Bu metot için Authrorize zorunlu değil.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "Authorize etiketi yok, Token kontrolü zorunlu değil.";
        }

    }
}
