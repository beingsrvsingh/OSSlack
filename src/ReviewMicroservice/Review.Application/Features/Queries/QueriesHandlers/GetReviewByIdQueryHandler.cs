using MediatR;
using Microsoft.Extensions.Logging;
using Review.Application.Services;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class GetReviewByIdQueryHandler : IRequestHandler<GetReviewByIdQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<GetReviewByIdQueryHandler> _logger;

        public GetReviewByIdQueryHandler(IReviewService reviewService, ILogger<GetReviewByIdQueryHandler> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.ReviewId <= 0)
            {
                _logger.LogWarning("GetReviewByIdQuery is null or has invalid ID.");
                return Result.Failure(new FailureResponse("InvalidRequest", "The request is invalid."));
            }

            var result = await _reviewService.GetByReviewIdAsync(request.ReviewId);

            if (result == null)
            {
                _logger.LogWarning("Review not found: {ReviewId}", request.ReviewId);
                return Result.Failure(new FailureResponse("NotFound", "Review not found."));
            }

            _logger.LogInformation("Fetched review {ReviewId}", request.ReviewId);
            return Result.Success(result);
        }
    }
}
