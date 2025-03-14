using Identity.Application.Features.User.Commands.CreateUser;
using Identity.Application.Services.Interfaces;
using MediatR;

namespace Identity.Application.Features.User.Commands.CommandHandler
{
    internal class SeedRoleCommandHandler : IRequestHandler<SeedRoleCommand>
    {
        private readonly ISeedService seedService;

        public SeedRoleCommandHandler(ISeedService seedService)
        {
            this.seedService = seedService;
        }

        public async Task Handle(SeedRoleCommand request, CancellationToken cancellationToken)
        {
            await seedService.CreateRoleSync();            
        }
    }
}
