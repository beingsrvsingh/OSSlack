using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class GetPoojasByTempleQueryHandler : IRequestHandler<GetPoojasByTempleQuery, Result>
{
    private readonly IPoojaService _service;
    private readonly ILoggerService<GetPoojasByTempleQueryHandler> _logger;

    public GetPoojasByTempleQueryHandler(IPoojaService service, ILoggerService<GetPoojasByTempleQueryHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(GetPoojasByTempleQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var poojas = await _service.GetPoojasByTempleAsync(request.TempleId);
            if (!poojas.Any())
                return Result.Failure("No poojas found for this temple.");
            return Result.Success(poojas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch poojas for temple id: {request.TempleId}");
            return Result.Failure("Unable to retrieve poojas.");
        }
    }
}
