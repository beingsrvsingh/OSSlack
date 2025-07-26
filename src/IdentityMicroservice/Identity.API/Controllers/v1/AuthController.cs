using BaseApi;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.API.Controllers.v1
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly ILoggerService<AuthController> loggerService;
        public AuthController(ILoggerService<AuthController> loggerService)
        {
            this.loggerService = loggerService;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("register/email")]
        public async Task<IActionResult> Register([FromBody] CreateUserEmailCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return new ConflictObjectResult(response.Errors);

            await Mediator.Publish(new CreatedUserEmailEvent { UserName = request.Email, Email = request.Email });

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost, Route("register/phone")]
        public async Task<IActionResult> RegisterUsingPhone([FromBody] CreateUserPhoneCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return new ConflictObjectResult(response.Errors);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("login/email-password")]
        public async Task<IActionResult> LoginUsingEmailPassword([FromBody] LoginUserEmailPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);


            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("login/email")]
        public async Task<IActionResult> LoginUsingEmail([FromBody] LoginUserEmailCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);


            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("login/phone")]
        public async Task<IActionResult> LoginUsingPhone([FromBody] LoginUserPhoneCommand request)
        {
            var response = await Mediator.Send(request);
            if (!response.Succeeded)
                return NotFound(response);


            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("generate/otp/email")]
        public async Task<IActionResult> GenerateOtpUsingEmail([FromBody] LoginUserEmailPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("verify/otp/email")]
        public async Task<IActionResult> VerifyOtpUsingEmail([FromBody] LoginUserEmailPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("generate/otp/phone")]
        public async Task<IActionResult> GenerateOtpUsingPhone([FromBody] LoginUserEmailPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("verify/otp/phone")]
        public async Task<IActionResult> VerifyOtpUsingPhone([FromBody] LoginUserEmailPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost, Route("set-password")]
        public async Task<IActionResult> SetPassword([FromBody] SetPasswordCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [HttpPost("activate-user")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ToggleUserActivation([FromBody] ToggleUserActivationRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Email) && string.IsNullOrWhiteSpace(request.PhoneNumber.ToString()))
            {
                return BadRequest(Result.Failure("Provide either Email or PhoneNumber."));
            }

            Result result;

            if (!string.IsNullOrWhiteSpace(request.Email))
            {
                var command = new LoginUserEmailCommand { Email = request.Email };
                result = await Mediator.Send(command);
            }
            else
            {
                var command = new LoginUserPhoneCommand { PhoneNumber = request.PhoneNumber };
                result = await Mediator.Send(command);
            }

            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

    }
}