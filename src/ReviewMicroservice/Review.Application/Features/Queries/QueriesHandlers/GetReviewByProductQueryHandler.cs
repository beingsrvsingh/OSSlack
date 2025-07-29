using MediatR;
using Review.Application.Features.Queries;
using Review.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class GetReviewByProductQueryHandler : IRequestHandler<GetReviewsByProductQuery, Result>
{
    private readonly IReviewService _reviewService;
    private readonly ILoggerService<GetReviewByProductQueryHandler> _logger;

    public GetReviewByProductQueryHandler(IReviewService reviewService, ILoggerService<GetReviewByProductQueryHandler> logger)
    {
        _reviewService = reviewService;
        _logger = logger;
    }

    public async Task<Result> Handle(GetReviewsByProductQuery request, CancellationToken cancellationToken)
    {
        var (items, totalCount) = await _reviewService.GetReviewsByProductAsync(request);

        if (totalCount == 0)
        {
            _logger.LogInfo("No reviews found for ProductId {ProductId} on page {Page}", request.ProductId, request.Page);
            return Result.Success($"No reviews found for this product on page {request.Page}.");
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
