using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Queries
{
    public class GetSearchQueryHandler : IRequestHandler<GetSearchQuery, Result>
    {
        private readonly ILoggerService<GetSearchQueryHandler> logger;
        private readonly IAstrologerService astrologerService;

        public GetSearchQueryHandler(ILoggerService<GetSearchQueryHandler> logger, IAstrologerService astrologerService)
        {
            this.logger = logger;
            this.astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetSearchQuery request, CancellationToken cancellationToken)
        {
            var searchResult = await astrologerService.SearchAsync(request.Query, request.Page, request.PageSize, cancellationToken);

            if (searchResult == null || searchResult.Results == null || !searchResult.Results.Any())
            {
                return Result.Success(new FailureResponse("No record found", "no record found"));
            }

            return Result.Success(searchResult);
        }

    }
}
