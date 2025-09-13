using Mapster;
using MediatR;
using SearchAggregator.Application.Features.Command;
using SearchAggregator.Application.Services;
using SearchAggregator.Domain.Entities;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Commands
{
    public class AddSearchUserHistoryCommandHandler : IRequestHandler<AddSearchUserHistoryCommand, Result>
    {
        private readonly ILoggerService<AddSearchUserHistoryCommandHandler> logger;
        private readonly ISearchService searchService;

        public AddSearchUserHistoryCommandHandler(ILoggerService<AddSearchUserHistoryCommandHandler> logger, ISearchService searchService)
        {
            this.logger = logger;
            this.searchService = searchService;
        }

        public async Task<Result> Handle(AddSearchUserHistoryCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userSearch = request.Adapt<UserSearchHistory>();
                var searchHistory = await searchService.AddAsync(userSearch);

                if (searchHistory != null)
                    return Result.Success();
                else
                    return Result.Failure(new FailureResponse("Error", "Failed to add user search history"));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Exception in AddSearchUserHistoryCommandHandler");
                return Result.Failure(new FailureResponse("Error", "Exception occurred while adding user search history"));
            }
        }

    }
}
