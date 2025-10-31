using MediatR;
using Pooja.Application.Features.Queries;
using Pooja.Application.Services;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

public class GetPoojaByIdQueryHandler : IRequestHandler<GetPoojaByIdQuery, Result>
{
    private readonly IPoojaService _service;
    private readonly ILoggerService<GetPoojaByIdQueryHandler> _logger;

    public GetPoojaByIdQueryHandler(IPoojaService service, ILoggerService<GetPoojaByIdQueryHandler> logger)
    {
        _service = service;
        _logger = logger;
    }

    public async Task<Result> Handle(GetPoojaByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var pooja = await _service.GetPoojaByIdAsync(request.Id);
            if (pooja == null)
                return Result.Failure("Pooja not found.");
            return Result.Success(pooja);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch pooja with id: {request.Id}");
            return Result.Failure("Unable to retrieve pooja.");
        }
    }
}
