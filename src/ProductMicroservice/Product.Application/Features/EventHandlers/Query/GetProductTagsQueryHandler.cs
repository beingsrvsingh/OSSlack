using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetProductTagsQueryHandler : IRequestHandler<GetProductTagsQuery, IEnumerable<ProductTagMaster>>
    {
        private readonly ILoggerService<GetProductTagsQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetProductTagsQueryHandler(
            ILoggerService<GetProductTagsQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IEnumerable<ProductTagMaster>> Handle(GetProductTagsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productService.GetTagsAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetProductTagsQueryHandler", ex);
                return Enumerable.Empty<ProductTagMaster>();
            }
        }
    }

}