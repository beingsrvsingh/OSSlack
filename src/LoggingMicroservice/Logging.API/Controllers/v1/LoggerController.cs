using BaseApi;
using Logging.Application.Features.Command;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Logging.Controllers.v1
{
    [Authorize]
    public class LoggerController : BaseController
    {
        [HttpGet]
        public IActionResult Get(ISender sender)
        {
            Mediator.Send(sender);
            return Ok();
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("Log")]
        public IActionResult Post(LogCommand command) 
        {
            Mediator.Send(command);
            return Ok();
        }
    }
}
