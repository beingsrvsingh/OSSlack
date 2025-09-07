using BaseApi;
using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kathavachak.API.Controllers.v1
{
    public class KathavachakTimeSlotController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateKathavachakTimeSlotCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);
            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Time slot created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Update([FromBody] UpdateKathavachakTimeSlotCommand request)
        {
            var result = await Mediator.Send(request);
            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Time slot updated successfully." });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteKathavachakTimeSlotCommand(id));
            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Time slot deleted successfully." });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await Mediator.Send(new GetKathavachakTimeSlotByIdQuery(id));
            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllKathavachakTimeSlotsQuery());
            return Ok(result);
        }
    }

}
