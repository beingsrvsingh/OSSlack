using Identity.Application.Features.User.Commands.UserAddress;
using MediatR;
using Identity.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class UpdateUserAddressCommandHandler : IRequestHandler<UpdateUserAddressCommand, Result>
    {
        private readonly IUserService userService;

        public UpdateUserAddressCommandHandler(IUserService userService)
        {
            this.userService = userService;
        }
        public async Task<Result> Handle(UpdateUserAddressCommand request, CancellationToken cancellationToken)
        {
            var hasUpdated = await userService.UpdateUserAddressAsync(request);

            if (!hasUpdated)
            {
                return Result.Failure(new FailureResponse(
                    "AddressUpdateFailed",
                    "Failed to update user address. Please try again later."));
            }

            return Result.Success("User address updated successfully.");
        }
    }
}
