using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetFilteredProductsQueryHandler : IRequestHandler<GetFilteredProductsQuery, Result>
    {
        private readonly ILoggerService<GetFilteredProductsQueryHandler> _logger;
        private readonly IProductService _productService;
        private readonly ICatalogService catalogService;
        private readonly IReviewService reviewService;

        public GetFilteredProductsQueryHandler(
            ILoggerService<GetFilteredProductsQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService,
            IReviewService reviewService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
            this.reviewService = reviewService;
        }

        public async Task<Result> Handle(GetFilteredProductsQuery request, CancellationToken cancellationToken)
        {
            var response = await _productService.GetFilteredProductsAsync(
                request.AttributeId,
                request.PageNumber,
                request.PageSize,
                request.SortBy,
                request.SortDescending
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<ProductBySubCategoryResponseDto>());
            }

            var attributes = await catalogService.GetAttributesByCategoryId(
                Convert.ToInt32(request.CategoryId),
                Convert.ToInt32(request.SubCategoryId)
            );

            var productIds = response.Select(p => p.Id).Distinct().ToList();

            var reviewSummaries = await reviewService.GetProductReviewSummariesAsync(productIds);
            var summaryLookup = reviewSummaries.ToDictionary(r => r.ProductId);

            var grouped = response
                .GroupBy(r => r.Id)
                .Select(group =>
                {
                    var first = group.First();

                    // Lookup review summary, if missing, use defaults
                    summaryLookup.TryGetValue(group.Key, out var summary);

                    var entity = new ProductMaster
                    {
                        Id = group.Key,
                        Name = first.Name,
                        ThumbnailUrl = first.ThumbnailUrl,
                        CategoryId = first.CategoryId,
                        SubCategoryId = first.SubCategoryId,
                        Reviews = summary?.TotalReviews ?? 0,
                        Rating = (int)(summary?.AverageRating ?? 0),
                        CategoryNameSnapshot = first.CategoryName,
                        AttributeValues = group
                            .Where(g => !string.IsNullOrEmpty(g.AttributeKey))
                            .Select(g => new ProductAttributeValue
                            {
                                AttributeKey = g.AttributeKey,
                                AttributeLabel = g.AttributeLabel,
                                Value = g.Value ?? string.Empty
                            }).ToList()
                    };

                    return ProductBySubCategoryResponseDto.FromEntity(entity, attributes);
                })
                .ToList();

            return Result.Success(grouped);

        }


    }
}