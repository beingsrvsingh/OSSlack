using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class ReviewFeedbackCommandHandler : IRequestHandler<ReviewFeedbackCommand, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<ReviewFeedbackCommandHandler> _logger;

        public ReviewFeedbackCommandHandler(ILoggerService<ReviewFeedbackCommandHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(ReviewFeedbackCommand request, CancellationToken cancellationToken)
        {
            var success = await _reviewService.MarkReviewHelpfulAsync(request);
            if (!success)
                return Result.Failure(new FailureResponse("UpdateFailed", "Failed to update feedback"));

            return Result.Success("Feedback submitted successfully");
        }
    }
}