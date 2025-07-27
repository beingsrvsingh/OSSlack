using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventsHandler.CommandHandler;

public class RemoveEnvironmentVariableHandler : IRequestHandler<RemoveEnvironmentVariableCommand, Result>
{
    private readonly ILoggerService<RemoveEnvironmentVariableHandler> _logger;
    private readonly IEnvironmentService _envService;
    public RemoveEnvironmentVariableHandler(ILoggerService<RemoveEnvironmentVariableHandler> logger, IEnvironmentService envService)
    {
        this._logger = logger;
        _envService = envService;
    }

    public Task<Result> Handle(RemoveEnvironmentVariableCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _envService.RemoveVariable(request.Key);
            return Task.FromResult(Result.Success("Environment variable removed successfully."));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to remove environment variable {Key}.", request.Key);
            return Task.FromResult(Result.Failure(new FailureResponse("RemoveFailed", $"Failed to remove environment variable: {ex.Message}")));
        }
    }
}