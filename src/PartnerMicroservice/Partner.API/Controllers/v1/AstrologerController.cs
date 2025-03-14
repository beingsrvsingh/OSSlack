using BaseApi;
using Microsoft.AspNetCore.Mvc;
using Partner.Application.Features.Commands;

namespace Partner.API.Controllers.v1
{
    public class AstrologerController : BaseController
    {
        private readonly ILogger<AstrologerController> _logger;
        public AstrologerController(ILogger<AstrologerController> logger)
        {
            _logger = logger;
        }


        public async Task<IActionResult> AddAstrologer([FromBody] AstrolgerCommand command)
        {
            return new OkObjectResult(await Mediator.Send(command));
        }
    }
}
