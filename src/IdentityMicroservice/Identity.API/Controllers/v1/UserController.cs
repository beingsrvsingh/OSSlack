using BaseApi;
using Identity.Application.Contracts;
using Identity.Application.Features;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Features.User.Queries.UserAddress;
using Identity.Application.Features.User.Queries.UserInfo;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Identity.API.Controllers.v1
{
    [Authorize]
    public class UserController : BaseController
    {
        private readonly ILoggerService<UserController> loggerService;

        public UserController(ILoggerService<UserController> loggerService)
        {
            this.loggerService = loggerService;
        }

        [HttpGet("user")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser([FromQuery] string id)
        {
            var query = new GetUserInfoQuery { UserId = id };
            var result = await Mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result);
        }

        [HttpPut("user-info")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserInfo([FromBody] UpdateUserInfoRequest request)
        {
            var command = request.Adapt<UpdateUserInfoCommand>();
            command.UserId = UserId;

            var result = await Mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpGet("user-avatar")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAvatar([FromQuery] string id)
        {
            var query = new GetUserAvatarQuery { Id = id };
            var result = await Mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result);
        }

        [HttpPut("user-avatar")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserAvatar([FromBody] UpdateUserAvatarRequest request)
        {
            var command = request.Adapt<UpdateUserAvatarCommand>();
            command.UserId = UserId;

            var result = await Mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut, Route("deactivate-user")]
        public async Task<IActionResult> UserDeactivation([FromBody] DeactivateUserRequest request)
        {
            var command = new DeActivateUserCommand { IsActive = request.IsActive, UserId = UserId };
            await Mediator.Send(command);

            return Ok();
        }

        [HttpGet("user-address")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserAddressById([FromQuery] string id)
        {
            var query = new GetUserAddressByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            return result.Succeeded ? Ok(result) : NotFound(result);
        }

        [HttpPost("user-address")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAddress([FromBody] CreateUserAddressRequest request)
        {
            var command = request.Adapt<CreateUserAddressCommand>();
            command.UserId = UserId;

            var result = await Mediator.Send(command);
            return result.Succeeded ? CreatedAtAction(nameof(GetUserAddressById), new { id = command.UserId }, result) : BadRequest(result);
        }

        [HttpPut("user-address")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateUserAddress([FromBody] UpdateUserAddressRequest request)
        {
            var command = request.Adapt<UpdateUserAddressCommand>();
            command.UserId = UserId;

            var result = await Mediator.Send(command);
            return result.Succeeded ? Ok(result) : BadRequest(result);
        }

        [HttpDelete("user-address")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteUserAddress([FromQuery] string addressId)
        {
            var query = new DeleteUserAddressCommand
            {
                AddressId = addressId
            };
            var result = await Mediator.Send(query);
            return result.Succeeded ? Ok(result) : NotFound(result);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete, Route("delete-user")]
        public async Task<IActionResult> RemoveUserUsingEmail()
        {
            var command = new DeleteUserCommand { UserId = UserId };
            await Mediator.Send(command);

            return Ok();
        }        

    }
}
