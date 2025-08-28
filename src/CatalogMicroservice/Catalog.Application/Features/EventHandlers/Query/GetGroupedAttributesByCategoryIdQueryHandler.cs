using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetGroupedAttributesByCategoryIdQueryHandler: IRequestHandler<GetGroupedAttributesByCategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetGroupedAttributesByCategoryIdQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetGroupedAttributesByCategoryIdQueryHandler(ILoggerService<GetGroupedAttributesByCategoryIdQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetGroupedAttributesByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var attributes = await _service.GetGroupedAttributesAsync(request.CategoryId, request.SubCategoryId, request.IsSummary);

                if (attributes == null || !attributes.Any())
                {
                    return Result.Failure(new FailureResponse("Not Found", "No attributes found for this category"));
                }

                return Result.Success(attributes);
        }
    }
}