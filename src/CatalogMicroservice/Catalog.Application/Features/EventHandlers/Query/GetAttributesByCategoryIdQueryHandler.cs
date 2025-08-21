using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetAttributesByCategoryIdQueryHandler : IRequestHandler<GetAttributesByCategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetAttributesByCategoryIdQueryHandler> _logger;
        private readonly ICategoryService _categoryService;

        public GetAttributesByCategoryIdQueryHandler(
            ILoggerService<GetAttributesByCategoryIdQueryHandler> logger,
            ICategoryService categoryService)
        {
            _logger = logger;
            _categoryService = categoryService;
        }

        public async Task<Result> Handle(GetAttributesByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var attributes = await _categoryService.GetAttributesByCategoryIdAsync(request.CategoryId);

                if (attributes == null || !attributes.Any())
                {
                    return Result.Failure(new FailureResponse("Not Found", "No attributes found for this category"));
                }

                return Result.Success(attributes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error fetching attributes for CategoryId: {request.CategoryId}", ex);
                return Result.Failure(new FailureResponse("Server Error", ex.Message));
            }
        }
    }

}