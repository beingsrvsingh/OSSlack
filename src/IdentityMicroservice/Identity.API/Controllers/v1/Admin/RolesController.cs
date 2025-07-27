using BaseApi;
using Identity.Application.Features.Admin.Commands;
using Identity.Application.Features.User.Commands.CreateUser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1.Admin
{
    [Authorize (Roles = "Admin")]
    public class RolesController : BaseController
    {
        private readonly ILogger<RolesController> _logger;

        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;
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
