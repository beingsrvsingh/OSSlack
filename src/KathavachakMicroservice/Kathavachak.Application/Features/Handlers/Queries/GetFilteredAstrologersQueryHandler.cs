using MediatR;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Kathavachak.Application.Services;
using Kathavachak.Application.Features.Query;

namespace Kathavachak.Application.Features.EventHandlers.Query
{
    public class GetFilteredAstrologersQueryHandler : IRequestHandler<GetFilteredKathavachaksQuery, Result>
    {
        private readonly ILoggerService<GetFilteredAstrologersQueryHandler> _logger;
        private readonly IKathavachakService _kathavachakService;

        public GetFilteredAstrologersQueryHandler(
            ILoggerService<GetFilteredAstrologersQueryHandler> logger,
            IKathavachakService kathavachakService)
        {
            _logger = logger;
            _kathavachakService = kathavachakService;
        }

        public async Task<Result> Handle(GetFilteredKathavachaksQuery request, CancellationToken cancellationToken)
        {
            var response = await _kathavachakService.GetFilteredKathavachaksAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<CatalogResponseDto>());
            }
            return Result.Success(response);

        }


    }
}