using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Pooja.Application.Features.Commands;
using Pooja.Application.Features.Queries;

namespace Pooja.API.Controllers.v1
{
    public class PoojaController : BaseController
    {
        // Create Pooja
        [HttpPost("create-pooja")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePooja([FromBody] CreatePoojaCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Pooja created successfully." });
        }

        // Update Pooja
        [HttpPut("update-pooja")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePooja([FromBody] UpdatePoojaCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Pooja updated successfully." });
        }

        // Delete Pooja
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletePooja(int id)
        {
            var result = await Mediator.Send(new DeletePoojaCommand { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(new { Message = "Pooja deleted successfully." });
        }

        // Get Pooja by Id
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPoojaById(int id)
        {
            var result = await Mediator.Send(new GetPoojaByIdQuery { Id = id });

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        // Get All Poojas
        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllPoojas()
        {
            var result = await Mediator.Send(new GetAllPoojasQuery());
            return Ok(result);
        }

        // Get Poojas by Temple
        [HttpGet("temple/{templeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPoojasByTemple(int templeId)
        {
            var result = await Mediator.Send(new GetPoojasByTempleQuery { TempleId = templeId });
            return Ok(result);
        }

        // Get Poojas by Priest
        [HttpGet("priest/{priestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPoojasByPriest(int priestId)
        {
            var result = await Mediator.Send(new GetPoojasByPriestQuery { PriestId = priestId });
            return Ok(result);
        }

        // Search Poojas
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SearchPoojas([FromQuery] string keyword)
        {
            var result = await Mediator.Send(new SearchPoojasQuery { Keyword = keyword });
            return Ok(result);
        }
    }
}
