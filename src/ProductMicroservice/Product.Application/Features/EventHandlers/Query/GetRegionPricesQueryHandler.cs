using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetRegionPricesQueryHandler : IRequestHandler<GetRegionPricesQuery, IEnumerable<ProductRegionPriceMaster>>
    {
        private readonly ILoggerService<GetRegionPricesQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetRegionPricesQueryHandler(
            ILoggerService<GetRegionPricesQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductRegionPriceMaster>> Handle(GetRegionPricesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productService.GetRegionPricesAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetRegionPricesQueryHandler", ex);
                return Enumerable.Empty<ProductRegionPriceMaster>();
            }
        }
    }

}