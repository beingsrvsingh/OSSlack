using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Temple.Application.Features.Commands;
using Temple.Application.Features.Queries;

namespace Temple.API.Controllers.v1
{
    public class TempleAartiController : BaseController
    {
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTempleAartiByIdAsync(int id)
        {
            var result = await Mediator.Send(new GetTempleAartiByIdQuery(id));
            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTempleAarti([FromBody] CreateTempleAartiCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);
            if (!result.Succeeded)
                return Conflict(result);

            return Created(string.Empty, new { Message = "Temple aarti created successfully." });
        }

        [HttpPut("update/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateTempleAarti(int id, [FromBody] UpdateTempleAartiCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest("Id in route does not match Id in request body.");

            var result = await Mediator.Send(request);
            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Temple aarti updated successfully." });
        }

        [HttpDelete("delete/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteTempleAarti(int id)
        {
            var result = await Mediator.Send(new DeleteTempleAartiCommand(id));
            if (!result.Succeeded)
                return Conflict(result);

            return Ok(new { Message = "Temple aarti deleted successfully." });
        }
    }

}
