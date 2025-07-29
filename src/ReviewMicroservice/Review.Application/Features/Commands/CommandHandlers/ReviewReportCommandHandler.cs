using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class ReviewReportCommandHandler : IRequestHandler<ReviewReportCommand, Result>
    {
        private readonly ILoggerService<ReviewReportCommandHandler> _logger;
        private readonly IReviewService _reviewService;

        public ReviewReportCommandHandler(ILoggerService<ReviewReportCommandHandler> logger, IReviewService reviewService)
        {
            _logger = logger;
            this._reviewService = reviewService;
        }

        public async Task<Result> Handle(ReviewReportCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                _logger.LogWarning("ReviewReportCommand is null.");
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            var result = await _reviewService.ReportReviewAsync(command);
            if (!result)
            {
                _logger.LogWarning("Failed to report review {ReviewId}", command.ReviewId);
                return Result.Failure(new FailureResponse("ReportFailed", "Review report could not be submitted."));
            }

            return Result.Success("Review reported successfully.");
        }
    }
}