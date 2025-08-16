using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetSubCategoryByCategoryIdQueryHandler : IRequestHandler<GetSubCategoryByCategoryIdQuery, Result>
    {
        private readonly ILoggerService<GetSubCategoryByCategoryIdQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetSubCategoryByCategoryIdQueryHandler(ILoggerService<GetSubCategoryByCategoryIdQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetSubCategoryByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _service.GetSubCategoriesByCategoryIdAsync(request.Id);
            return category == null
                ? Result.Failure(new FailureResponse("Not Found", "Category not found"))
                : Result.Success(category);
        }
    }

}