using Logging.Application.Features.Command;
using Logging.Application.Service;
using Logging.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Handlers.Command
{
    public class LogIOSCommandHandler: IRequestHandler<LogIOSCommand, Result>
{
    private readonly ILogService logService;
    private readonly ILoggerService<LogIOSCommandHandler> logger;

    public LogIOSCommandHandler(ILogService logService, ILoggerService<LogIOSCommandHandler> logger)
    {
        this.logService = logService;
        this.logger = logger;
    }

    public async Task<Result> Handle(LogIOSCommand command, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(command.Message) || string.IsNullOrWhiteSpace(command.LogLevel))
        {
            return Result.Failure(new FailureResponse("InvalidInput", "Message and LogLevel are required."));
        }

        var log = command.Adapt<IOSLog>();

        try
        {
            var success = await logService.LogIOSAsync(log);
            if (!success)
            {
                return Result.Failure(new FailureResponse("LogFailure", "Failed to write iOS log."));
            }

            return Result.Success("iOS log recorded successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"{nameof(LogIOSCommandHandler)} Unexpected error occurred.");
            return Result.Failure(new FailureResponse("UnhandledException", "An unexpected error occurred."));
        }
    }
}
}