using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class SearchPoojasQueryHandler : IRequestHandler<SearchPoojasQuery, Result>
{
    private readonly IPoojaService _service;
    private readonly ILoggerService<SearchPoojasQueryHandler> _logger;

    public SearchPoojasQueryHandler(IPoojaService service, ILoggerService<SearchPoojasQueryHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(SearchPoojasQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var poojas = await _service.SearchPoojasAsync(request.Keyword);
            if (!poojas.Any())
                return Result.Failure("No poojas match the search keyword.");
            return Result.Success(poojas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to search poojas with keyword: {request.Keyword}");
            return Result.Failure("Unable to search poojas.");
        }
    }
}
