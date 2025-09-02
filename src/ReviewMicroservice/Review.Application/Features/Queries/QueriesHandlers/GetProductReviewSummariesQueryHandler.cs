using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class GetProductReviewSummariesQueryHandler : IRequestHandler<GetProductReviewSummariesQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<GetProductReviewSummariesQueryHandler> _logger;

        public GetProductReviewSummariesQueryHandler(ILoggerService<GetProductReviewSummariesQueryHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetProductReviewSummariesQuery request, CancellationToken cancellationToken)
        {
            var summary = await _reviewService.GetProductReviewSummariesAsync(request.Pids);

            if (summary == null)
            {
                _logger.LogInfo("No review summary found for ProductId {ProductId}", request.Pids);
                return Result.Failure(new FailureResponse("NotFound", "No review summary found for this product."));
            }

            return Result.Success(summary);
        }
    }
}