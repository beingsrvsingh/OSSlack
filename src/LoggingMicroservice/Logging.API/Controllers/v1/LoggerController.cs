using BaseApi;
using Logging.Application.Features.Command;
using Logging.Application.Features.Query;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;

namespace Logging.Controllers.v1
{
    public class LoggerController : BaseController
    {
        // ─────────────────────────────
        //        WRITE OPERATIONS
        // ─────────────────────────────

        [HttpPost("android")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogAndroid([FromBody] LogAndroidCommand command)
        {
            var result = await Mediator.Send(command);

            return result.Succeeded
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("ios")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogIOS([FromBody] LogIOSCommand command)
        {
            var result = await Mediator.Send(command);

            return result.Succeeded
                ? Ok(result)
                : BadRequest(result);
        }

        [HttpPost("web")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogWebService([FromBody] LogWebServiceCommand command)
        {
            var result = await Mediator.Send(command);

            return result.Succeeded
                ? Ok(result)
                : BadRequest(result);
        }

        // ─────────────────────────────
        //        READ OPERATIONS
        // ─────────────────────────────

        [HttpGet("android")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAndroidLogs([FromQuery] PaginatedQuery query)
        {        
            var result = await Mediator.Send(query);

            return result.Succeeded
                ? Ok(result)
                : NotFound(result);
        }

        [HttpGet("ios")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetIOSLogs([FromQuery] PaginatedQuery query)
        {
            var result = await Mediator.Send(query);

            return result.Succeeded
                ? Ok(result)
                : NotFound(result);
        }

        [HttpGet("web")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(FailureResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetWebServiceLogs([FromQuery] PaginatedQuery query)
        {
            var result = await Mediator.Send(query);

            return result.Succeeded
                ? Ok(result)
                : NotFound(result);
        }
    }
}
