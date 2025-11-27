using MediatR;
using Priest.Application.Features.Query;
using Priest.Application.Services;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Priest.Application.Features.EventHandlers.Query
{
    public class GetFilteredPoojasQueryHandler : IRequestHandler<GetFilteredQuery, Result>
    {
        private readonly ILoggerService<GetFilteredPoojasQueryHandler> _logger;
        private readonly IPriestService _priestService;

        public GetFilteredPoojasQueryHandler(
            ILoggerService<GetFilteredPoojasQueryHandler> logger,
            IPriestService priestService)
        {
            _logger = logger;
            _priestService = priestService;
        }

        public async Task<Result> Handle(GetFilteredQuery request, CancellationToken cancellationToken)
        {
            var response = await _priestService.GetFilteredAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<CatalogResponseDto>());
            }
            return Result.Success(response);

        }


    }
}