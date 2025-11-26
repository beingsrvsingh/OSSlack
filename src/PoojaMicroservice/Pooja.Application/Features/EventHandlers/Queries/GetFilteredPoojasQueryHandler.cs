using MediatR;
using Pooja.Application.Features.Query;
using Pooja.Application.Services;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.EventHandlers.Query
{
    public class GetFilteredPoojasQueryHandler : IRequestHandler<GetFilteredPoojasQuery, Result>
    {
        private readonly ILoggerService<GetFilteredPoojasQueryHandler> _logger;
        private readonly IPoojaService _poojaService;

        public GetFilteredPoojasQueryHandler(
            ILoggerService<GetFilteredPoojasQueryHandler> logger,
            IPoojaService poojaService)
        {
            _logger = logger;
            _poojaService = poojaService;
        }

        public async Task<Result> Handle(GetFilteredPoojasQuery request, CancellationToken cancellationToken)
        {
            var response = await _poojaService.GetFilteredPoojasAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<CatalogResponseDto>());
            }
            return Result.Success(response);

        }


    }
}