using MediatR;
using Identity.Application.Services.Interfaces;
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
            var user = await this.identityService.GetUserByIdAsync(request.UserId);

            if(user is null)
            {
                return Result.Failure("UserId is not valid.");
            }

            user.IsDeleted = true;
            await this.identityService.DeleteUserAsync(user);

            return Result.Success();
        }
    }
}
