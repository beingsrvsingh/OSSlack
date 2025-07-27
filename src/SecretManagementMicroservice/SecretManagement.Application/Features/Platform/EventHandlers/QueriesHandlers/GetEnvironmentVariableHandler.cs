using MediatR;
using SecretManagement.Application.Features.Queries;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class GetEnvironmentVariableHandler : IRequestHandler<GetEnvironmentVariableQuery, Result>
{
    private readonly ILoggerService<GetEnvironmentVariableHandler> _logger;
    private readonly IEnvironmentService _envService;
    public GetEnvironmentVariableHandler(ILoggerService<GetEnvironmentVariableHandler> logger, IEnvironmentService envService)
    {
        this._logger = logger;
        _envService = envService;
    }

    public Task<Result> Handle(GetEnvironmentVariableQuery request, CancellationToken cancellationToken)
    {
        var value = _envService.GetVariable(request.Key);

        if (string.IsNullOrWhiteSpace(value))
        {
            _logger.LogWarning("Environment variable '{Key}' was not found or is empty.", request.Key);
            return Task.FromResult(Result.Failure(new FailureResponse("NotFound", $"Environment variable '{request.Key}' not found or is empty.")));
        }

        return Task.FromResult(Result.Success(value));
    }
}
