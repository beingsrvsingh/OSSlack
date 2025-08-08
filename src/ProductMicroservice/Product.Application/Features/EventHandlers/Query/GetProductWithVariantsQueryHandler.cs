using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductWithVariantsQueryHandler : IRequestHandler<GetProductWithVariantsQuery, Result>
    {
        private readonly ILoggerService<GetProductWithVariantsQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetProductWithVariantsQueryHandler(
            ILoggerService<GetProductWithVariantsQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(GetProductWithVariantsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetProductWithVariantsAsync(request.ProductId);

                if (product is null)
                    return Result.Failure(new FailureResponse("NotFound", "Product not found"));

                var dto = product.Adapt<ProductDto>();
                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductWithVariantsQueryHandler", ex);
                return Result.Failure(new FailureResponse("Error", "An unexpected error occurred"));
            }
        }
    }

}