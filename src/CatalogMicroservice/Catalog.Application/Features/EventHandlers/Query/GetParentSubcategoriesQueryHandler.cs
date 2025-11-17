using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetParentSubcategoriesQueryHandler : IRequestHandler<GetParentSubcategoriesQuery, Result>
    {
        private readonly ILoggerService<GetParentSubcategoriesQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetParentSubcategoriesQueryHandler(ILoggerService<GetParentSubcategoriesQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetParentSubcategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetParentSubcategoriesAsync();
            return result == null
                ? Result.Failure(new FailureResponse("Failed", "No categories found"))
                : Result.Success(result);
        }
    }

}