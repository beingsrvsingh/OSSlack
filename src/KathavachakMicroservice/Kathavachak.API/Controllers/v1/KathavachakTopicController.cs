using BaseApi;
using Kathavachak.Application.Features.Commands;
using Kathavachak.Application.Features.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Kathavachak.API.Controllers.v1
{
    public class KathavachakTopicController : BaseController
    {
        [HttpPost("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateKathavachakTopic([FromBody] CreateKathavachakTopicCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Created(string.Empty, new { Message = "Topic created successfully." });
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateKathavachakTopic([FromBody] UpdateKathavachakTopicCommand request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await Mediator.Send(request);

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Topic updated successfully." });
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> DeleteKathavachakTopic(int id)
        {
            var result = await Mediator.Send(new DeleteKathavachakTopicCommand(id));

            if (!result.Succeeded)
                return Conflict(result.Errors);

            return Ok(new { Message = "Topic deleted successfully." });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetKathavachakTopicById(int id)
        {
            var result = await Mediator.Send(new GetKathavachakTopicByIdQuery(id));

            if (!result.Succeeded)
                return NotFound(result.Errors);

            return Ok(result);
        }

        [HttpGet("all")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllKathavachakTopics()
        {
            var result = await Mediator.Send(new GetAllKathavachakTopicsQuery());

            return Ok(result);
        }
    }

}
