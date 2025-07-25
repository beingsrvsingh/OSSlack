using BaseApi;
using Identity.Application.Features.Admin.Commands;
using Identity.Application.Features.User.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1.Admin
{
    public class RolesController : BaseController
    {
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-roles")]
        public async Task<IActionResult> CreateRole()
        {
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();
            var machineName = Environment.MachineName;
            var hostName = System.Net.Dns.GetHostName();

            var result = await Mediator.Send(new SeedRoleCommand());

            if (result.Succeeded)
            {
                return Created(string.Empty, result.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [Route("users/{userId}/roles")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserEmailRoleCommand request)
        {
            return new OkObjectResult(await Mediator.Send(request));
        }
    }
}
