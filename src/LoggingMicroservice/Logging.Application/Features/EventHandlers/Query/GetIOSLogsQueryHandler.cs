using Logging.Application.Features.Query;
using Logging.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Logging.Application.Features.EventHandlers.Query
{
    public class GetIOSLogsQueryHandler : IRequestHandler<PaginatedQuery, Result>
    {
        private readonly ILogService logService;
        private readonly ILoggerService<GetIOSLogsQueryHandler> logger;

        public GetIOSLogsQueryHandler(ILogService logService, ILoggerService<GetIOSLogsQueryHandler> logger)
        {
            this.logService = logService;
            this.logger = logger;
        }

        public async Task<Result> Handle(PaginatedQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await logService.GetIOSLogsAsync(query.Page, query.PageSize);

                if (result.TotalCount == 0)
                {
                    return Result.Failure(
                        new FailureResponse("NoLogsFound", "No iOS logs found."));
                }

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch iOS logs.");
                return Result.Failure(
                    new FailureResponse("UnhandledException", "Unexpected error occurred while reading iOS logs."));
            }
        }
    }

}