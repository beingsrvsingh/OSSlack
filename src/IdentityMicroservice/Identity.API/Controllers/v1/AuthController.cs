using BaseApi;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.ChangePassword;
using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Domain.Events;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.API.Controllers.v1
{
    [AllowAnonymous]
    public class AuthController : BaseController
    {
        private readonly ILoggerService loggerService;
        public AuthController(ILoggerService loggerService)
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
        public async Task<IActionResult> GenerateOtpUsingEmail([FromBody] LoginUserEmailCommand request)
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
        public async Task<IActionResult> VerifyOtpUsingEmail([FromBody] LoginUserEmailCommand request)
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
        public async Task<IActionResult> GenerateOtpUsingPhone([FromBody] LoginUserEmailCommand request)
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
        public async Task<IActionResult> VerifyOtpUsingPhone([FromBody] LoginUserEmailCommand request)
        {
            var response = await Mediator.Send(request);

            if (!response.Succeeded)
                return NotFound(response);

            return Ok(response);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete, Route("remove/email")]
        public async Task<IActionResult> RemoveUserUsingEmail([FromBody] DeleteUserCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpDelete, Route("remove/phone")]
        public async Task<IActionResult> RemoveUserUsingPhone([FromBody] DeleteUserCommand request)
        {
            await Mediator.Send(request);

            return Ok();
        }

        [Authorize]
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

    }
}