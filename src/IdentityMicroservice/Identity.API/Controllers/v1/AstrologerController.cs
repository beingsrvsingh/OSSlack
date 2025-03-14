using BaseApi;
using Identity.Application.Features.Admin.Commands;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Domain.Events;
using Microsoft.AspNetCore.Mvc;

namespace Identity.API.Controllers.v1
{
    public class AstrologerController : BaseController
    {
        private readonly ILogger<AstrologerController> logger;
        public AstrologerController(ILogger<AstrologerController> logger)
        {
            this.logger = logger;
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("register-astrolger")]
        public async Task<IActionResult> AddAstrologer([FromBody] CreateUserEmailCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return new ConflictObjectResult(response.Errors);

            await Mediator.Publish(new CreatedUserEmailEvent { UserName = request.Email, Email = request.Email });

            return Created();
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("verify-astrolger")]
        public async Task<IActionResult> VerifyAstrolgerEmail([FromBody] VerificationAstrologerCommand command)
        {
            return new OkObjectResult(await Mediator.Send(command));
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("astrologer-profile")]
        public async Task<IActionResult> AddAstrologerDetails([FromBody] AstrolgerProfileCommand command)
        {
            return new OkObjectResult(await Mediator.Send(command));
        }
    }
}
