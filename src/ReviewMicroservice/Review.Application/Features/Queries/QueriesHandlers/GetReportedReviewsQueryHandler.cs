using Microsoft.Extensions.Logging;
using Review.Application.Services;
using Shared.Utilities.Response;
using MediatR;

namespace Review.Application.Features.Queries.QueryHandlers
{
    public class GetReportedReviewsQueryHandler : IRequestHandler<GetReportedReviewsQuery, Result>
    {
        private readonly IReviewService _reviewService;
        private readonly ILogger<GetReportedReviewsQueryHandler> _logger;

        public GetReportedReviewsQueryHandler(
            IReviewService reviewService,
            ILogger<GetReportedReviewsQueryHandler> logger)
        {
            _reviewService = reviewService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetReportedReviewsQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await _reviewService.GetReportedReviewsAsync(request.Page, request.PageSize);

            if (items == null || items.Count == 0)
            {
                _logger.LogInformation("No reported reviews found on page {Page}", request.Page);
                return Result.Success($"No reported reviews found on page {request.Page}.");
            }

            return Result.Success(new
            {
                Items = items,
                TotalCount = totalCount,
                Page = request.Page,
                PageSize = request.PageSize
            });
        }
    }
}
