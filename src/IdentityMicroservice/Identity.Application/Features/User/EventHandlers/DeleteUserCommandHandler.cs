using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, Result>
    {
        private readonly IIdentityService identityService;

        public DeleteUserCommandHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }
        public async Task<Result> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await identityService.GetUserByIdAsync(request.UserId);

            if (user is null)
            {
                return Result.Failure(new FailureResponse(
                    "UserNotFound",
                    $"User with ID {request.UserId} does not exist."));
            }

            user.IsDeleted = true;
            await identityService.DeleteUserAsync(user);

            return Result.Success("User deleted successfully.");
        }
    }
}
