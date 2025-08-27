using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, Result>
    {
        private readonly ILoggerService<GetProductQueryHandler> _logger;
        private readonly IProductService _productService;
        private readonly ICatalogService catalogService;

        public GetProductQueryHandler(
            ILoggerService<GetProductQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
        }

        public async Task<Result> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetProductWithAttributesAsync(request.productId);

                if (product == null)
                    return Result.Failure(new FailureResponse("NotFound", "No products found"));

                var attributes = await catalogService.GetAttributesByCategoryId(request.CategoryId, request.SubCategoryId, request.IsSummary);

                var dtoList = ProductSummaryResponseDto.FromGroupedAttributeEntity(product, attributes);

                return Result.Success(dtoList);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in GetProductBySubCategoryId handler");
                return Result.Failure(new FailureResponse("Error", "An error occurred while processing the request."));
            }
        }
    }
}