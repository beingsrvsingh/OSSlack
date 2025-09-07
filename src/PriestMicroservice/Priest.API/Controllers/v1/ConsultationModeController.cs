using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Priest.Application.Features.Commands;
using Priest.Application.Features.Query;

namespace Priest.API.Controllers.v1
{
    public class ConsultationModeController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateConsultationModeCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return Conflict(result);

            return Created(string.Empty, new { Message = "Consultation mode created." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateConsultationModeCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Consultation mode updated." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteConsultationModeCommand { Id = id });

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Consultation mode deleted." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllConsultationModesQuery());

            if (!result.Succeeded) return BadRequest(result);

            return Ok(result);
        }
    }
}
