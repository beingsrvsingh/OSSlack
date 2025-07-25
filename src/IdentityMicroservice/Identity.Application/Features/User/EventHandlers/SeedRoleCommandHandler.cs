using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Utilities.Response;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    internal class SeedRoleCommandHandler : IRequestHandler<SeedRoleCommand, Result>
    {
        private readonly ISeedService seedService;

        public SeedRoleCommandHandler(ISeedService seedService)
        {
            this.seedService = seedService;
        }

        public async Task<Result> Handle(SeedRoleCommand request, CancellationToken cancellationToken)
        {
            var success = await seedService.CreateRoleSync();

            if (success)
            {
                return Result.Success("Roles seeded successfully.");
            }

            return Result.Failure(new FailureResponse(
                "RoleSeedingFailed",
                "Some or all roles could not be created. Please check the logs for details."
            ));
        }
    }
}
