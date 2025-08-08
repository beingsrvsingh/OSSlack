using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductAttributesQueryHandler : IRequestHandler<GetProductAttributesQuery, IEnumerable<ProductAttributeMaster>>
    {
        private readonly ILoggerService<GetProductAttributesQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetProductAttributesQueryHandler(
            ILoggerService<GetProductAttributesQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductAttributeMaster>> Handle(GetProductAttributesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productService.GetAttributesAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductAttributesQueryHandler", ex);
                return Enumerable.Empty<ProductAttributeMaster>();
            }
        }
    }

}