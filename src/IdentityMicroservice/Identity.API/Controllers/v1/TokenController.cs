using BaseApi;
using Identity.Application.Features.User.Commands.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    [AllowAnonymous]
    public class TokenController : BaseController
    {

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost, Route("refresh-token")]
        public async Task<IActionResult> RefreshToken(RefreshTokenCommand request)
        {
            var response = await Mediator.Send(request);

            if (response.Succeeded)
                return new OkObjectResult(response);

            return new NotFoundObjectResult(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("revoke-token")]
        public async Task<IActionResult> RevokeToken(RevokeUserCommand command)
        {
            var response = await Mediator.Send(command);

            if(response.Succeeded)
                return Ok(new { message = "Token revoked" });

            return new BadRequestObjectResult(response);
        }
    }
}