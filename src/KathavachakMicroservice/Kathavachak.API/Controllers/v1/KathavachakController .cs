using BaseApi;
using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kathavachak.API.Controllers.v1
{
    public class KathavachakMasterController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateKathavachakMaster([FromBody] CreateKathavachakMasterCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Kathavachak created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateKathavachakMaster([FromBody] UpdateKathavachakMasterCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Kathavachak updated successfully." });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteKathavachakMaster(int id)
        {
            var result = await Mediator.Send(new DeleteKathavachakMasterCommand(id));

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Kathavachak deleted successfully." });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetKathavachakMasterById(int id)
        {
            var result = await Mediator.Send(new GetKathavachakMasterByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllKathavachakMasters()
        {
            var result = await Mediator.Send(new GetAllKathavachakMastersQuery());

            return Ok(result);
        }
    }

}
