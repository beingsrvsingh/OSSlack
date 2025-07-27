using BaseApi;
using Identity.Application.Features.Admin.Commands;
using Identity.Application.Features.User.Commands.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1.Admin
{
    public class SeedsController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-roles")]
        public async Task<IActionResult> SeedRole()
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
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("seed-locations")]
        public async Task<IActionResult> SeedCountriesStatesAndCities()
        {
            var result = await Mediator.Send(new SeedLocationCommand());

            if (result.Succeeded)
            {
                return Created(string.Empty, result.Data);
            }

            return StatusCode(StatusCodes.Status500InternalServerError, result);
        }
    }
}