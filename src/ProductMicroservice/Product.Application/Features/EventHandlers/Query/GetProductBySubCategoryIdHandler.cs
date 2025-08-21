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

        public GetProductBySubCategoryIdHandler(
            ILoggerService<GetLocalizedInfoQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(GetProductBySubCategoryId request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _productService.GetProductBySubCategoryIdAsync(request.SubCategoryId);
                if (product is null)
                    return Result.Failure(new FailureResponse("NotFound", "Product not found"));

                var dto = ProductBySubCategoryResponseDto.FromEntityList(product);
                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetLocalizedInfoQueryHandler", ex);
                return Result.Failure(new FailureResponse("NOT FOUND", "No Records Found."));
            }
        }
    }
}