using Temple.Application.Features.Admin.Commands;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class SeedAstrologerExpertiesCommandHandler : IRequestHandler<SeedAstrologerExpertiesCommand, Result>
    {
        private readonly ILoggerService<SeedAstrologerExpertiesCommandHandler> _logger;
        private readonly ISeedService seedService;

        public SeedAstrologerExpertiesCommandHandler(ILoggerService<SeedAstrologerExpertiesCommandHandler> logger, ISeedService seedService)
        {
            this._logger = logger;
            this.seedService = seedService;
        }

        public async Task<Result> Handle(SeedAstrologerExpertiesCommand request, CancellationToken cancellationToken)
        {
            var success = await seedService.SeedAstrologerExpertiesAsync();

            if (!success)
            {
                _logger.LogWarning("Seeding astrologer experties failed.");
                return Result.Failure(new FailureResponse("SeedingFailed", "Failed to seed astrologer experties."));
            }

            return Result.Success("Astrologer experties seeded successfully.");
        }
    }
}