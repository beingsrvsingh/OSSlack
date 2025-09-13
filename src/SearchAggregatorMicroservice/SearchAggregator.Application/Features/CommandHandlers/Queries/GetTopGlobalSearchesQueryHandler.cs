using MediatR;
using Microsoft.Extensions.Logging;
using SearchAggregator.Application.Features.Query;
using SearchAggregator.Domain.Core.Repository;
using SearchAggregator.Domain.Entities;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.CommandHandlers.Queries
{
    public class GetTopGlobalSearchesQueryHandler : IRequestHandler<GetTopGlobalSearchesQuery, Result>
    {
        private readonly IUserSearchHistoryRepository _repository;
        private readonly ILogger<GetTopGlobalSearchesQueryHandler> _logger;

        public GetTopGlobalSearchesQueryHandler(
            IUserSearchHistoryRepository repository,
            ILogger<GetTopGlobalSearchesQueryHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTopGlobalSearchesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _repository.GetTopGlobalSearchesAsync(request.TopN);
                if (result == null)
                {
                    return Result.Success(new List<UserSearchHistory>());
                }
                return Result.Success(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get top {TopN} global searches", request.TopN);
                return Result.Failure(new FailureResponse("SOMETHING_WENT_WRONG", "something went wrong."));
            }
        }
    }

}
