using BaseApi;
using Identity.Application.Features.Admin.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1.Admin
{
    /// <summary>
    /// This Controller responsible to add data to database
    /// </summary>
    [Authorize]
    public class AdminsController : BaseController
    {        
        [HttpPost, Route("country")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCountry([FromBody] CountryCommand command)
        {
            if (command == null)
                return BadRequest("Country data is required.");

            var response = await Mediator.Send(command);

            if (!response.Succeeded)
                return BadRequest(response);

            return Created(string.Empty, response);
        }

        [HttpPost, Route("state")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddState([FromBody] StateCommand command)
        {
            if (command == null)
                return BadRequest("State data is required.");

            var response = await Mediator.Send(command);

            if (!response.Succeeded)
                return BadRequest(response);

            return Created(string.Empty, response);
        }

        [HttpPost, Route("city")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCity([FromBody] CityCommand command)
        {
            if (command == null)
                return BadRequest("City data is required.");

            var response = await Mediator.Send(command);

            if (!response.Succeeded)
                return BadRequest(response);

            return Created(string.Empty, response);
        }

    }
}
