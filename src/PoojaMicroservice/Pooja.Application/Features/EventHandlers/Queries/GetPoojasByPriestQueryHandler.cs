using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class GetPoojasByPriestQueryHandler : IRequestHandler<GetPoojasByPriestQuery, Result>
{
    private readonly IPoojaService _service;
    private readonly ILoggerService<GetPoojasByPriestQueryHandler> _logger;

    public GetPoojasByPriestQueryHandler(IPoojaService service, ILoggerService<GetPoojasByPriestQueryHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(GetPoojasByPriestQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var poojas = await _service.GetPoojasByPriestAsync(request.PriestId);
            if (!poojas.Any())
                return Result.Failure("No poojas found for this priest.");
            return Result.Success(poojas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch poojas for priest id: {request.PriestId}");
            return Result.Failure("Unable to retrieve poojas.");
        }
    }
}
