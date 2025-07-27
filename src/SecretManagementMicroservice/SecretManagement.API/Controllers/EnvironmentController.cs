using BaseApi;
using Microsoft.AspNetCore.Mvc;
using SecretManagement.Application.Contracts;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Features.Queries;

namespace SecretManagement.API.Controllers
{
    public class EnvironmentController : BaseController
    {
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("get/{key}")]
        public async Task<IActionResult> GetVariable(string key)
        {
            var result = await Mediator.Send(new GetEnvironmentVariableQuery(key));

            if (!result.Succeeded)
                return NotFound(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("is-set/{key}")]
        public async Task<IActionResult> IsSet(string key)
        {
            var result = await Mediator.Send(new IsEnvironmentVariableSetQuery(key));
            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost("set")]
        public async Task<IActionResult> SetVariable([FromBody] EnvironmentVariableRequest request)
        {
            var result = await Mediator.Send(new SetEnvironmentVariableCommand(request.Key, request.Value));

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete("remove/{key}")]
        public async Task<IActionResult> RemoveVariable(string key)
        {
            var result = await Mediator.Send(new RemoveEnvironmentVariableCommand(key));

            if (!result.Succeeded)
                return BadRequest(result);

            return Ok(result);
        }
    }
}