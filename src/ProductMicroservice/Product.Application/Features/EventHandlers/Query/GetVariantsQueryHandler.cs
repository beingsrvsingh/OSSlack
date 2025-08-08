using Mapster;
using MediatR;
using Product.Application.Contracts;
using Product.Application.Features.Query;
using Product.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetVariantsQueryHandler : IRequestHandler<GetVariantsQuery, Result>
    {
        private readonly ILoggerService<GetVariantsQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetVariantsQueryHandler(
            ILoggerService<GetVariantsQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<Result> Handle(GetVariantsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var variants = await _productService.GetVariantsAsync(request.ProductId);

                if (variants == null || !variants.Any())
                {
                    return Result.Failure(new FailureResponse("NotFound", "No variants found."));
                }

                var dto = variants.Adapt<IEnumerable<ProductVariantDto>>();

                return Result.Success(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetVariantsQueryHandler", ex);
                return Result.Failure(new FailureResponse("Error", "An unexpected error occurred."));
            }
        }

    }

}