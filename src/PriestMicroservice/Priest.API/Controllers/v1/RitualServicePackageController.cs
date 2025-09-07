using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Priest.Application.Features.Commands;
using Priest.Application.Features.Query;

namespace Priest.API.Controllers.v1
{
    public class RitualServicePackageController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRitualServicePackageCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return Conflict(result);

            return Created(string.Empty, new { Message = "Ritual service package created." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateRitualServicePackageCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Ritual service package updated." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeleteRitualServicePackageCommand { Id = id });

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Ritual service package deleted." });
        }

        [HttpGet("priest/{priestId:int}")]
        public async Task<IActionResult> GetByPriest(int priestId)
        {
            var result = await Mediator.Send(new GetRitualServicePackagesByPriestIdQuery { PriestId = priestId });

            if (!result.Succeeded) return NotFound(result);

            return Ok(result);
        }
    }
}
