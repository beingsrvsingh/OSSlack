using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class DeleteReviewCommandHandler : IRequestHandler<DeleteReviewCommand, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<DeleteReviewCommandHandler> _logger;

        public DeleteReviewCommandHandler(IReviewService reviewService, ILoggerService<DeleteReviewCommandHandler> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogWarning("DeleteReviewCommand is null.");
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            var review = await _reviewService.GetByReviewIdAsync(request.ReviewId);
            if (review == null)
            {
                _logger.LogWarning("Review not found for deletion. ReviewId: {ReviewId}", request.ReviewId);
                return Result.Failure(new FailureResponse("NotFound", "Review not found"));
            }

            if (review.UserId != request.RequestingUserId)
            {
                _logger.LogWarning("Unauthorized deletion attempt by user {UserId} for review {ReviewId}", request.RequestingUserId, request.ReviewId);
                return Result.Failure(new FailureResponse("Unauthorized", "Cannot delete another user's review"));
            }

            var hasDeleted = await _reviewService.DeleteAsync(review);
            if (!hasDeleted)
                return Result.Failure(new FailureResponse("DeleteFailed", "Unable to delete the review."));

            _logger.LogInfo("Review {ReviewId} deleted successfully by user {UserId}", request.ReviewId, request.RequestingUserId);

            return Result.Success("Review deleted successfully.");
        }

    }
}