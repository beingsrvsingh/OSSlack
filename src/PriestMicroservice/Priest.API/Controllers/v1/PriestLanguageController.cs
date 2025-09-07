using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Priest.Application.Features.Commands;
using Priest.Application.Features.Query;

namespace Priest.API.Controllers.v1
{
    public class PriestLanguageController : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePriestLanguageCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return Conflict(result);

            return Created(string.Empty, new { Message = "Priest language created." });
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdatePriestLanguageCommand command)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = await Mediator.Send(command);

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Priest language updated." });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await Mediator.Send(new DeletePriestLanguageCommand { Id = id });

            if (!result.Succeeded) return NotFound(result);

            return Ok(new { Message = "Priest language deleted." });
        }

        [HttpGet("priest/{priestId:int}")]
        public async Task<IActionResult> GetByPriest(int priestId)
        {
            var result = await Mediator.Send(new GetPriestLanguagesQuery { PriestId = priestId });

            if (!result.Succeeded) return NotFound(result);

            return Ok(result);
        }
    }
}
