using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductSEOInfoQueryHandler : IRequestHandler<GetProductSEOInfoQuery, ProductSEOInfoMaster?>
    {
        private readonly ILoggerService<GetProductSEOInfoQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetProductSEOInfoQueryHandler(
            ILoggerService<GetProductSEOInfoQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<ProductSEOInfoMaster?> Handle(GetProductSEOInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productService.GetSEOInfoAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductSEOInfoQueryHandler", ex);
                return null;
            }
        }
    }

}