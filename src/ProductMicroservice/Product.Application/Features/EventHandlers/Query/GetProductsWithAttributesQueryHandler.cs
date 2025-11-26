using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductsWithAttributesQueryHandler : IRequestHandler<GetProductsBySubcategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetLocalizedInfoQueryHandler> _logger;
        private readonly IProductService _productService;
        private readonly ICatalogService catalogService;
        private readonly IReviewService reviewService;

        public GetProductsWithAttributesQueryHandler(
            ILoggerService<GetLocalizedInfoQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService,
            IReviewService reviewService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
            this.reviewService = reviewService;
        }

        public async Task<Result> Handle(GetProductsBySubcategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productService.GetProductBySubCategoryIdAsync(request.SubCategoryId);

                if (products == null)
                    return Result.Failure(new FailureResponse("NotFound", "No products found"));

                //var attributes = await catalogService.GetAttributesByCategoryId(request.CategoryId, request.SubCategoryId, request.IsSummary);

                //// Get all product IDs
                //var productIds = products.Select(p => p.Id).ToList();

                //// Fetch all review summaries in one call
                //var reviewSummaries = await reviewService.GetProductReviewSummariesAsync(productIds);

                //// Create a lookup for quick access
                //var summaryLookup = reviewSummaries.ToDictionary(r => r.ProductId);

                //// Map summaries back to products
                //foreach (var product in products)
                //{
                //    if (summaryLookup.TryGetValue(product.Id, out var summary))
                //    {
                //        product.Reviews = summary.TotalReviews;
                //        product.Rating = (int)summary.AverageRating;
                //    }
                //    else
                //    {
                //        product.Reviews = 0;
                //        product.Rating = 0;
                //    }
                //}


                //var dtoList = ProductBySubCategoryResponseDto.FromEntityList(products, attributes!);

                return Result.Success(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetProductsWithAttributes handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }


    }
}