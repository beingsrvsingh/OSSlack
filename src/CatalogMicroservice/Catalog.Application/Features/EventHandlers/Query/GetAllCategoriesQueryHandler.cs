using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, Result>
    {
        private readonly ILoggerService<GetAllCategoriesQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetAllCategoriesQueryHandler(ILoggerService<GetAllCategoriesQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetAllCategoriesAsync();
            return result == null
                ? Result.Failure(new FailureResponse("Failed", "No categories found"))
                : Result.Success(result);
        }
    }

}