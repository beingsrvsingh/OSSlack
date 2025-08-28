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
            _logger.LogInfo("Fetching filterable attributes for CategoryId: {CategoryId}, SubCategoryId: {SubCategoryId}",
                request.CategoryId, request.SubCategoryId);

            var attributes = await _service.GetFilterableAttributes(request.CategoryId, request.SubCategoryId);

            if (attributes == null || attributes.Count == 0)
            {
                _logger.LogWarning("No filterable attributes found for CategoryId: {CategoryId}, SubCategoryId: {SubCategoryId}",
                    request.CategoryId, request.SubCategoryId);

                return Result.Success(new List<object>());
            }

            _logger.LogInfo("Successfully fetched {Count} filterable attributes.", attributes.Count);
            return Result.Success(attributes);
        }

    }
}