using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductBySubCategoryIdHandler : IRequestHandler<GetProductBySubCategoryId, Result>
    {
        private readonly ILoggerService<GetLocalizedInfoQueryHandler> _logger;
        private readonly IProductService _productService;
        private readonly ICatalogService catalogService;

        public GetProductBySubCategoryIdHandler(
            ILoggerService<GetLocalizedInfoQueryHandler> logger,
            IProductService productService,
            ICatalogService catalogService)
        {
            _logger = logger;
            _productService = productService;
            this.catalogService = catalogService;
        }

        public async Task<Result> Handle(GetProductBySubCategoryId request, CancellationToken cancellationToken)
        {
            try
            {
                var products = await _productService.GetProductBySubCategoryIdAsync(request.SubCategoryId);

                if (products == null || !products.Any())
                    return Result.Failure(new FailureResponse("NotFound", "No products found"));

                var attributes = await catalogService.GetAttributesBySubCategoryIdAsync(request.SubCategoryId);

                var dtoList = ProductBySubCategoryResponseDto.FromEntityList(products, attributes);

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