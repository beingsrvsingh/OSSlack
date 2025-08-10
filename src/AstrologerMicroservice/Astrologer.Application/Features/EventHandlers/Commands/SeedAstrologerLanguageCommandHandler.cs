using AstrologerMicroservice.Application.Features.Admin.Commands;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Commands.CommandHandlers
{
    public class SeedAstrologerLanguageCommandHandler : IRequestHandler<SeedAstrologerLanguageCommand, Result>
    {
        private readonly ILoggerService<SeedAstrologerLanguageCommandHandler> _logger;
        private readonly ISeedService seedService;

        public SeedAstrologerLanguageCommandHandler(ILoggerService<SeedAstrologerLanguageCommandHandler> logger, ISeedService seedService)
        {
            this._logger = logger;
            this.seedService = seedService;
        }

        public async Task<Result> Handle(SeedAstrologerLanguageCommand request, CancellationToken cancellationToken)
        {
            var success = await seedService.SeedAstrologerLanguagesAsync();

            if (!success)
            {
                _logger.LogWarning("Seeding astrologer languages failed.");
                return Result.Failure(new FailureResponse("SeedingFailed", "Failed to seed astrologer languages."));
            }

            return Result.Success("Astrologer languages seeded successfully.");
        }
    }
}