using Logging.Application.Features.Command;
using Logging.Application.Service;
using Logging.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Logging.Applicaton.Features.Handlers.Command
{
    public class LogAndroidCommandHandler : IRequestHandler<LogAndroidCommand, Result>
    {
        private readonly ILogService logService;
        private readonly ILoggerService<LogAndroidCommandHandler> logger;

        public LogAndroidCommandHandler(ILogService logService, ILoggerService<LogAndroidCommandHandler> logger)
        {
            this.logService = logService;
            this.logger = logger;
        }

        public async Task<Result> Handle(LogAndroidCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Message) || string.IsNullOrWhiteSpace(command.LogLevel))
            {
                return Result.Failure(new FailureResponse("InvalidInput", "Message and LogLevel are required."));
            }

            var log = command.Adapt<AndroidLog>();

            try
            {
                var success = await logService.LogAndroidAsync(log);
                if (!success)
                {
                    return Result.Failure(new FailureResponse("LogFailure", "Failed to write Android log."));
                }

                return Result.Success("Android log recorded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(LogAndroidCommandHandler)} Unexpected error occurred.");
                return Result.Failure(new FailureResponse("UnhandledException", "An unexpected error occurred."));
            }
        }
    }

}
