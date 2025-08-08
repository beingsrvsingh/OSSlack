using MediatR;
using Product.Application.Features.Query;
using Product.Application.Services;
using Product.Domain.Entities;
using Shared.Application.Interfaces.Logging;

namespace Product.Application.Features.EventHandlers.Query
{
    public class GetLocalizedInfoQueryHandler : IRequestHandler<GetLocalizedInfoQuery, IEnumerable<LocalizedProductInfoMaster>>
    {
        private readonly ILoggerService<GetLocalizedInfoQueryHandler> _logger;
        private readonly IProductService _productService;

        public GetLocalizedInfoQueryHandler(
            ILoggerService<GetLocalizedInfoQueryHandler> logger,
            IProductService productService)
        {
            _logger = logger;
            _productService = productService;
        }

        public async Task<IEnumerable<LocalizedProductInfoMaster>> Handle(GetLocalizedInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return await _productService.GetLocalizedInfoAsync(request.ProductId);
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception in GetLocalizedInfoQueryHandler", ex);
                return Enumerable.Empty<LocalizedProductInfoMaster>();
            }
        }
    }

}