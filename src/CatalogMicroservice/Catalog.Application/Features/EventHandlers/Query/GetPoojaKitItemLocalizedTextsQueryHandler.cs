using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetPoojaKitItemLocalizedTextsQueryHandler : IRequestHandler<GetPoojaKitItemLocalizedTextsQuery, Result>
{
    private readonly ILoggerService<GetPoojaKitItemLocalizedTextsQueryHandler> _logger;
    private readonly IPoojaKitItemService _service;

    public GetPoojaKitItemLocalizedTextsQueryHandler(ILoggerService<GetPoojaKitItemLocalizedTextsQueryHandler> logger, IPoojaKitItemService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(GetPoojaKitItemLocalizedTextsQuery request, CancellationToken cancellationToken)
    {
        var result = await _service.GetLocalizedTextsAsync(request.ItemId);
        return result.Any() ? Result.Success(result) : Result.Failure("No localized texts found.");
    }
}
}