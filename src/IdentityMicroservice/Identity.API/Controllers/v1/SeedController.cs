using BaseApi;
using Identity.Application.Features.User.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    [Authorize]
    public class SeedController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("CreateRole")]
        public async Task<IActionResult> CreateRole()
        {

            var x = HttpContext.Request.Headers["User-Agent"];
            var z = System.Environment.MachineName;
            var y = System.Net.Dns.GetHostName();

            //await Mediator.Send(new SeedRoleCommand());
            return Created();
        }
    }
}
