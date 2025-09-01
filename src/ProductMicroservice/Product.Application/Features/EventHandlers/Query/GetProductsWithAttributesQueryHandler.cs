using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductsWithAttributesQueryHandler : IRequestHandler<GetProductsWithAttributesQuery, Result>
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

        public async Task<Result> Handle(GetProductsWithAttributesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productService.GetProductBySubCategoryIdAsync(request.SubCategoryId);

                if (products == null || products.Count == 0)
                    return Result.Failure(new FailureResponse("NotFound", "No products found"));

                var attributes = await catalogService.GetAttributesByCategoryId(request.CategoryId, request.SubCategoryId, request.IsSummary);

                for (int i = 0; i < products.Count; i++)
                {
                    var review = await reviewService.GetProductReviewSummaryAsync(products[i].Id);
                    products[i].Reviews = review.TotalReviews;
                    products[i].Rating = (int)review.AverageRating;
                }

                var dtoList = ProductBySubCategoryResponseDto.FromEntityList(products, attributes!);

                return Result.Success(dtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetProductsWithAttributes handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }


    }
}