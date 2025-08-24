using Address.Application.Features.Commands;
using BaseApi;
using Microsoft.AspNetCore.Mvc;

namespace Address.API.Controllers.v1
{
    public class SeedController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-address")]
        public async Task<IActionResult> SeedAstrologerLanguage()
        {
            var result = await Mediator.Send(new SeedAddressCommand());

            if (result.Succeeded)
            {
                return Created(string.Empty, result.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}