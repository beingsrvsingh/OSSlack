using MediatR;
using Review.Application.Contracts;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class GetReviewsByUserQueryHandler : IRequestHandler<GetReviewsByUserQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<GetReviewsByUserQueryHandler> _logger;

        public GetReviewsByUserQueryHandler(ILoggerService<GetReviewsByUserQueryHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetReviewsByUserQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _reviewService.GetReviewsByUserAsync(request);

            if (items == null || items.Count == 0)
            {
                _logger.LogInfo("No reviews found for user {UserId} on page {Page}", request.UserId, request.Page);
                return Result.Success(new GetReviewsByUserResponse
                {
                    Reviews = new List<ReviewDto>(),
                    TotalCount = 0
                });
            }

            var response = new GetReviewsByUserResponse
            {
                Reviews = items,
                TotalCount = totalCount
            };

            return Result.Success(response);
        }

    }
}
