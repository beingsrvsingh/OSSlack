using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Priest.Application.Features.Commands;
using Priest.Application.Features.Query;

namespace Priest.API.Controllers.v1
{
    public class TimeSlotController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTimeSlotCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return Conflict(result);

            return Created(string.Empty, new { Message = "Time slot created." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTimeSlotCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Time slot updated." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteTimeSlotCommand { Id = id });

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Time slot deleted." });
        }

        [HttpGet("schedule/{scheduleId:int}")]
        public async Task<IActionResult> GetBySchedule(int scheduleId)
        {
            var result = await Mediator.Send(new GetTimeSlotsByScheduleIdQuery { ScheduleId = scheduleId });

            if (!result.Succeeded) return NotFound(result);

            return Ok(result);
        }
    }
}
