using BaseApi;
using Configuration.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Configuration.Controllers
{
    public class ConfigurationController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [HttpPost]
        [Route("security-token")]
        public async Task<IActionResult> GetSecurityToken(GetUserSecurityTokenQuery req)
        {
            var response = await Mediator.Send(req);

            return Ok(response);
        }
    }
}
