using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class AddReviewCommandHandler : IRequestHandler<AddReviewCommand, Result>
    {
        private readonly ILoggerService<AddReviewCommandHandler> _logger;
        private readonly IReviewService _reviewService;

        public AddReviewCommandHandler(
            ILoggerService<AddReviewCommandHandler> logger,
            IReviewService reviewService)
        {
            _logger = logger;
            _reviewService = reviewService;
        }

        public async Task<Result> Handle(AddReviewCommand command, CancellationToken cancellationToken)
        {
            if (command == null)
            {
                _logger.LogWarning("ReviewCommand is null.");
                return Result.Failure(new FailureResponse("InvalidRequest", "The request payload cannot be null."));
            }

            if (command.Rating < 1 || command.Rating > 5)
            {
                _logger.LogWarning("Invalid rating {Rating} for product {ProductId} by user {UserId}", command.Rating, command.ProductId, command.UserId);
                return Result.Failure(new FailureResponse("InvalidRating", "Rating must be between 1 and 5."));
            }

            var result = await _reviewService.AddReviewAsync(command);

            if (!result)
            {
                _logger.LogWarning("Failed to add review. A review already exists for product {ProductId} by user {UserId}", command.ProductId, command.UserId);
                return Result.Failure(new FailureResponse("AlreadyExists", "You have already submitted a review for this product."));
            }

            _logger.LogInfo("Review added successfully for product {ProductId} by user {UserId}", command.ProductId, command.UserId);
            return Result.Success("Review added successfully.");
        }

    }

}