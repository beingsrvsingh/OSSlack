using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Application.Interfaces;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class CreateUserAddressCommandHandler : IRequestHandler<CreateUserAddressCommand, Result>
    {
        private readonly ILoggerService<CreateUserAddressCommandHandler> _logger;
        private readonly IUserService userService;

        public CreateUserAddressCommandHandler(ILoggerService<CreateUserAddressCommandHandler> logger, IUserService userService)
        {
            this._logger = logger;
            this.userService = userService;
        }

        public async Task<Result> Handle(CreateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var result = await userService.CreateUserAddressAsync(request);

            if (!result)
            {
                return Result.Failure(new FailureResponse(
                    "AddressCreationFailed",
                        "We couldn't create the user's address due to a system error. Please try again later or contact support if the issue persists."));
            }

            _logger.LogInfo("Successfully created user address for user {UserId}", request.UserId);
            return Result.Success("User address created successfully.");
        }
    }
}
