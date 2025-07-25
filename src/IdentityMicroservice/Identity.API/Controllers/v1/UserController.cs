using BaseApi;
using Identity.Application.Contracts;
using Identity.Application.Features.User.Commands;
using Identity.Application.Features.User.Commands.UserAddress;
using Identity.Application.Features.User.Commands.UserInfo;
using Identity.Application.Features.User.Queries.UserAddress;
using Identity.Application.Features.User.Queries.UserInfo;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared.Application.Interfaces.Logging;

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

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet, Route("user")]
        public async Task<IActionResult> GetUser([FromQuery]GetUserInfoQuery query)
        {
            return new OkObjectResult(await Mediator.Send(query));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut, Route("user-info")]
        public async Task<IActionResult> UpdateUserInfo(UpdateUserInfoRequest request)
        {
            var command = request.Adapt<UpdateUserInfoCommand>();
            command.UserId = UserId;
            return new OkObjectResult(await Mediator.Send(command));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet, Route("user-avatar")]
        public async Task<IActionResult> GetUserAvatar([FromQuery] GetUserAvatarQuery query)
        {
            return new OkObjectResult(await Mediator.Send(query));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut, Route("user-avatar")]
        public async Task<IActionResult> UpdateUserAvatar(UpdateUserAvatarRequest request)
        {
            var command = request.Adapt<UpdateUserAvatarCommand>();
            command.UserId = UserId;
            command.AvatarURI = "https://demo.com/avatar1.jpg";
            return new OkObjectResult(await Mediator.Send(command));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet, Route("user-address")]
        public async Task<IActionResult> GetUserAddressById([FromQuery]GetUserAddressByIdQuery query)
        {
            return new OkObjectResult(await Mediator.Send(query));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost, Route("user-address")]
        public async Task<IActionResult> CreateUserAddress(CreateUserAddressCommand request)
        {
            return new OkObjectResult(await Mediator.Send(request));
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut, Route("user-address")]
        public async Task<IActionResult> UpdateUserAddress(UpdateUserAddressRequest request)
        {
            var command = request.Adapt<UpdateUserAddressCommand>();
            command.UserId = UserId;
            return new OkObjectResult(await Mediator.Send(command));
        }
    }
}
