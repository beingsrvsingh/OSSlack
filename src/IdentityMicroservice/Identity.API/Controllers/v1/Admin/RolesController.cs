using BaseApi;
using Identity.Application.Features.Admin.Commands;
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
        [HttpPost, Route("CreateRoles")]
        public async Task<IActionResult> Post([FromBody] RoleCommand request)
        {
            return new OkObjectResult(await Mediator.Send(request));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("UserMapRole")]
        public async Task<IActionResult> UserMapRole([FromBody] UserRoleCommand request)
        {
            return new OkObjectResult(await Mediator.Send(request));
        }
    }
}
