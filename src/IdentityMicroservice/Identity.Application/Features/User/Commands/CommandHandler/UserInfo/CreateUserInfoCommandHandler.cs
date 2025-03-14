using Identity.Application.Features.User.Commands.UserInfo;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler.UserInfo
{
    public class CreateUserInfoCommandHandler : IRequestHandler<CreateUserInfoCommand, Result>
    {
        private readonly IUserService userService;

        public CreateUserInfoCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(CreateUserInfoCommand request, CancellationToken cancellationToken)
        {
            await userService.CreateUserInfoAsync(request, cancellationToken);
            return Result.Success();
        }
    }
}
