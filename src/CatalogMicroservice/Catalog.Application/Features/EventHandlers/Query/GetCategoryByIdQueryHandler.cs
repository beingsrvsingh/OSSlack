using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, Result>
    {
        private readonly ILoggerService<GetCategoryByIdQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetCategoryByIdQueryHandler(ILoggerService<GetCategoryByIdQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _service.GetCategoryByIdAsync(request.Id);
            return category == null
                ? Result.Failure(new FailureResponse("Not Found", "Category not found"))
                : Result.Success(category);
        }
    }

}