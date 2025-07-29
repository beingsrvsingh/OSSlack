using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class UpdateReviewCommandHandler : IRequestHandler<UpdateReviewCommand, Result>
    {
        private readonly ILoggerService<UpdateReviewCommandHandler> _logger;
        private readonly IReviewService _reviewService;

        public UpdateReviewCommandHandler(
            ILoggerService<UpdateReviewCommandHandler> logger,
            IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        public async Task<Result> Handle(UpdateReviewCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                _logger.LogWarning("UpdateReviewCommand is null.");
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            if (command.ReviewId <= 0)
            {
                _logger.LogWarning("ReviewId is missing in update command.");
                return Result.Failure(new FailureResponse("InvalidRequest", "ReviewId must be provided for updating a review."));
            }

            if (command.Rating < 1 || command.Rating > 5)
            {
                _logger.LogWarning("Invalid rating {Rating} for review {ReviewId}", command.Rating, command.ReviewId);
                return Result.Failure(new FailureResponse("InvalidRating", "Rating must be between 1 and 5."));
            }

            var result = await _reviewService.UpdateReviewAsync(command);
            if (!result)
            {
                _logger.LogWarning("Failed to update review {ReviewId}", command.ReviewId);
                return Result.Failure(new FailureResponse("UpdateFailed", "Review could not be updated. Either it doesn't exist or user is not authorized."));
            }

            _logger.LogInfo("Review updated successfully: {ReviewId}", command.ReviewId);
            return Result.Success("Review updated successfully.");
        }
    }

}