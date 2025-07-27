using BaseApi;
using Identity.Application.Features.User.Commands.Token;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    [AllowAnonymous]
    public class TokenController : BaseController
    {
        /// <summary>
        /// Generates a new access token using a valid refresh token.
        /// </summary>
        [HttpPost("refresh-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenCommand request)
        {
            var response = await Mediator.Send(request);

            if (response.Succeeded)
                return Ok(response);

            return BadRequest(response);
        }

        /// <summary>
        /// Revoke a refresh token for the specified user.
        /// </summary>
        [HttpDelete("revoke-token")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RevokeToken([FromBody] RevokeUserCommand command)
        {
            var response = await Mediator.Send(command);

            if (response.Succeeded)
                return Ok(response);

            return BadRequest(response);
        }

    }
}