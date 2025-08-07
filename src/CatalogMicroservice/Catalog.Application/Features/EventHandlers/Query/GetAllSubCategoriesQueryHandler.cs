using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetAllSubCategoriesQueryHandler : IRequestHandler<GetAllSubCategoriesQuery, Result>
{
    private readonly ILoggerService<GetAllSubCategoriesQueryHandler> _logger;
    private readonly ISubCategoryService _service;

    public GetAllSubCategoriesQueryHandler(ILoggerService<GetAllSubCategoriesQueryHandler> logger, ISubCategoryService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(GetAllSubCategoriesQuery request, CancellationToken cancellationToken)
    {
        var data = await _service.GetAllSubCategoriesAsync();
        return data.Any() ? Result.Success(data) : Result.Failure("No subcategories found.");
    }
}
}