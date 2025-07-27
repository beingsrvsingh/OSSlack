using MediatR;
using SecretManagement.Application.Features.Queries;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class IsEnvironmentVariableSetHandler : IRequestHandler<IsEnvironmentVariableSetQuery, Result>
{
    private readonly ILoggerService<IsEnvironmentVariableSetHandler> _logger;
    private readonly IEnvironmentService _envService;
    public IsEnvironmentVariableSetHandler(ILoggerService<IsEnvironmentVariableSetHandler> logger, IEnvironmentService envService)
    {
        this._logger = logger;
        _envService = envService;
    }

    public Task<Result> Handle(IsEnvironmentVariableSetQuery request, CancellationToken cancellationToken)
    {
        var isSet = _envService.IsSet(request.Key);

        _logger.LogInfo("Environment variable '{Key}' is {Status}.", request.Key, isSet ? "set" : "not set");

        return Task.FromResult(Result.Success(isSet));
    }
}