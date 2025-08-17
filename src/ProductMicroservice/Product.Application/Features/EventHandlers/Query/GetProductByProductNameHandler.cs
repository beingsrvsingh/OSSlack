using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductByProductNameHandler : IRequestHandler<GetProductByProductName, Result>
    {
        private readonly ILoggerService<GetLocalizedInfoQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetProductByProductNameHandler(
            ILoggerService<GetLocalizedInfoQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(GetProductByProductName request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetProductByProductNameAsync(request.ProductName);
                if (product is null)
                    return Result.Failure(new FailureResponse("NotFound", "Product not found"));

                var dto = product.Adapt<ProductDto>();
                return Result.Success(product);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetLocalizedInfoQueryHandler", ex);
                return Result.Failure(new FailureResponse("NOT FOUND", "No Records Found."));
            }
        }
    }
}