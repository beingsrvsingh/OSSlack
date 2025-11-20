using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetFilterableAttributesQueryHandler : IRequestHandler<GetFilterableAttributesQuery, Result>
    {
        private readonly ILoggerService<GetFilterableAttributesQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetFilterableAttributesQueryHandler(ILoggerService<GetFilterableAttributesQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetFilterableAttributesQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching filterable attributes for SubCategoryId: {SubCategoryId}",
                request.Scid);

            var attributes = await _service.GetFilterableAttributes(request.Scid);

            if (attributes == null && attributes.Attributes.Count == 0)
            {
                _logger.LogWarning("No filterable attributes found.");

                return Result.Success(new List<object>());
            }

            _logger.LogInfo("Successfully fetched {Count} filterable attributes.", attributes.Attributes.Count);
            return Result.Success(attributes);
        }

    }
}