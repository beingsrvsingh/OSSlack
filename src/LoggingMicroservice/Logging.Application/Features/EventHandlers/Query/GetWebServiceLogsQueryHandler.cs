using Logging.Application.Features.Query;
using Logging.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Logging.Application.Features.EventHandlers.Query
{
    public class GetWebServiceLogsQueryHandler : IRequestHandler<PaginatedQuery, Result>
    {
        private readonly ILogService logService;
        private readonly ILoggerService<GetWebServiceLogsQueryHandler> logger;

        public GetWebServiceLogsQueryHandler(ILogService logService, ILoggerService<GetWebServiceLogsQueryHandler> logger)
        {
            this.logService = logService;
            this.logger = logger;
        }

        public async Task<Result> Handle(PaginatedQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var result = await logService.GetWebServiceLogsAsync(query.Page, query.PageSize);

                if (result.TotalCount == 0)
                    return Result.Failure(
                        new FailureResponse("NoLogsFound", "No WebService logs found."));

                return Result.Success(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to fetch WebService logs.");
                return Result.Failure(
                    new FailureResponse("UnhandledException", "Unexpected error occurred while reading WebService logs."));
            }
        }
    }

}