using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class SeedReviewReportReasonCommandHandler : IRequestHandler<SeedReviewReportReasonCommand, Result>
    {
        private readonly ILoggerService<SeedReviewReportReasonCommandHandler> _logger;
        private readonly ISeedService seedService;

        public SeedReviewReportReasonCommandHandler(ILoggerService<SeedReviewReportReasonCommandHandler> logger, ISeedService seedService)
        {
            this._logger = logger;
            this.seedService = seedService;
        }

        public async Task<Result> Handle(SeedReviewReportReasonCommand request, CancellationToken cancellationToken)
        {
            var success = await seedService.SeedReviewReportReasonsAsync();

            if (!success)
            {
                _logger.LogWarning("Seeding review report reasons failed");
                return Result.Failure(new FailureResponse("SeedingFailed", "Failed to seed review report reasons."));
            }

            return Result.Success("Review report reasons seeded successfully.");
        }
    }
}