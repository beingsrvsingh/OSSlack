using Identity.Application.Features;
using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Utilities.Response;

public class DeleteUserAddressCommandHandler : IRequestHandler<DeleteUserAddressCommand, Result>
{
    private readonly IUserService userService;

    public DeleteUserAddressCommandHandler(IUserService userService)
    {
        this.userService = userService;
    }

    public async Task<Result> Handle(DeleteUserAddressCommand request, CancellationToken cancellationToken)
    {
        var address = await userService.GetUserAddressById(Convert.ToInt32(request.AddressId));

        if (address == null)
        {
            return Result.Failure(new FailureResponse("UserAddressNotFound", "Address not found for the user."));
        }

        await userService.DeleteUserAddressAsync(Convert.ToInt32(request.AddressId));

        return Result.Success("Address deleted successfully.");
    }
}
