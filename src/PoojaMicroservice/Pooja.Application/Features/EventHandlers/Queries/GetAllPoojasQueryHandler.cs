using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class GetAllPoojasQueryHandler : IRequestHandler<GetAllPoojasQuery, Result>
{
    private readonly IPoojaService _service;
    private readonly ILoggerService<GetAllPoojasQueryHandler> _logger;

    public GetAllPoojasQueryHandler(IPoojaService service, ILoggerService<GetAllPoojasQueryHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(GetAllPoojasQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var poojas = await _service.GetAllPoojasAsync();
            if (!poojas.Any())
                return Result.Failure("No poojas found.");
            return Result.Success(poojas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to fetch poojas.");
            return Result.Failure("Unable to retrieve poojas.");
        }
    }
}
