
using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventsHandler.CommandHandler;

public class SetEnvironmentVariableHandler : IRequestHandler<SetEnvironmentVariableCommand, Result>
{
    private readonly IEnvironmentService _envService;
    private readonly ILoggerService<SetEnvironmentVariableHandler> _logger;
    public SetEnvironmentVariableHandler(ILoggerService<SetEnvironmentVariableHandler> logger, IEnvironmentService envService)
    {
        _envService = envService;
        this._logger = logger;
    }

    public Task<Result> Handle(SetEnvironmentVariableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _envService.SetVariable(request.Key, request.Value ?? "");
            return Task.FromResult(Result.Success("Environment variable was set successfully."));

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to set environment variable {Key}.", request.Key);
            return Task.FromResult(Result.Failure(new FailureResponse("SetEnvVarFailed", "Failed to set environment variable.")));
        }
    }
}