using BaseApi;
using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kathavachak.API.Controllers.v1
{
    public class KathavachakSessionModeController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateKathavachakSessionMode([FromBody] CreateKathavachakSessionModeCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Session mode created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateKathavachakSessionMode([FromBody] UpdateKathavachakSessionModeCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Session mode updated successfully." });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteKathavachakSessionMode(int id)
        {
            var result = await Mediator.Send(new DeleteKathavachakSessionModeCommand(id));

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Session mode deleted successfully." });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetKathavachakSessionModeById(int id)
        {
            var result = await Mediator.Send(new GetKathavachakSessionModeByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllKathavachakSessionModes()
        {
            var result = await Mediator.Send(new GetAllKathavachakMediaQuery());

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }
    }
}
