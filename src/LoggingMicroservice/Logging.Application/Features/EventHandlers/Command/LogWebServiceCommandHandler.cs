using Logging.Application.Features.Command;
using Logging.Application.Service;
using Logging.Domain.Entities;
using Mapster;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Logging.Application.Features.Handlers.Command
{
    public class LogWebServiceCommandHandler : IRequestHandler<LogWebServiceCommand, Result>
    {
        private readonly ILogService logService;
        private readonly ILoggerService<LogWebServiceCommandHandler> logger;

        public LogWebServiceCommandHandler(ILogService logService, ILoggerService<LogWebServiceCommandHandler> logger)
        {
            this.logService = logService;
            this.logger = logger;
        }

        public async Task<Result> Handle(LogWebServiceCommand command, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(command.Message) || string.IsNullOrWhiteSpace(command.LogLevel))
            {
                return Result.Failure(new FailureResponse("InvalidInput", "Message and LogLevel are required."));
            }

            var log = command.Adapt<WebServiceLog>();

            try
            {
                var success = await logService.LogWebServiceAsync(log);
                if (!success)
                {
                    return Result.Failure(new FailureResponse("LogFailure", "Failed to write WebService log."));
                }

                return Result.Success("WebService log recorded successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"{nameof(LogWebServiceCommandHandler)} Unexpected error occurred.");
                return Result.Failure(new FailureResponse("UnhandledException", "An unexpected error occurred."));
            }
        }
    }

}