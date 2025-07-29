using MediatR;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Review.Application.Features.Queries.QueriesHandlers
{
    public class GetProductReviewSummaryQueryHandler : IRequestHandler<GetProductReviewSummaryQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILoggerService<GetProductReviewSummaryQueryHandler> _logger;

        public GetProductReviewSummaryQueryHandler(ILoggerService<GetProductReviewSummaryQueryHandler> logger, IReviewService reviewService)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetProductReviewSummaryQuery request, CancellationToken cancellationToken)
        {
            var summary = await _reviewService.GetProductReviewSummaryAsync(request);

            if (summary == null)
            {
                _logger.LogInfo("No review summary found for ProductId {ProductId}", request.ProductId);
                return Result.Failure(new FailureResponse("NotFound", "No review summary found for this product."));
            }

            return Result.Success(summary);
        }
    }
}