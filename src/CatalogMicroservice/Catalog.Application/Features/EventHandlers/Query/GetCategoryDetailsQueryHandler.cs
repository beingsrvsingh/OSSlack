using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetCategoryDetailsQueryHandler : IRequestHandler<GetCategoryDetailsQuery, Result>
    {
        private readonly ILoggerService<GetCategoryDetailsQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetCategoryDetailsQueryHandler(ILoggerService<GetCategoryDetailsQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetCategoryDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var category = await _service.GetCategoryDetails(request.CategoryIds, request.SubCategoryIds);
                return category == null
                    ? Result.Failure(new FailureResponse("Not Found", "Category not found"))
                    : Result.Success(category);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting category details");
                return Result.Failure(new FailureResponse("Server Error", "Unable to fetch category details"));
            }
        }
    }
}
