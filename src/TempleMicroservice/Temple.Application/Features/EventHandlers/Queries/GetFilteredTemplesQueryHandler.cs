using MediatR;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Query;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Query
{
    public class GetFilteredTemplesQueryHandler : IRequestHandler<GetFilteredTemplesQuery, Result>
    {
        private readonly ILoggerService<GetFilteredTemplesQueryHandler> _logger;
        private readonly ITempleService templeService;
        private readonly ICatalogService catalogService;

        public GetFilteredTemplesQueryHandler(
            ILoggerService<GetFilteredTemplesQueryHandler> logger,
            ITempleService templeService,
            ICatalogService catalogService)
        {
            _logger = logger;
            templeService = templeService;
            this.catalogService = catalogService;
        }

        public async Task<Result> Handle(GetFilteredTemplesQuery request, CancellationToken cancellationToken)
        {
            var response = await templeService.GetFilteredTemplesAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<CatalogResponseDto>());
            }
            return Result.Success(response);

        }


    }
}