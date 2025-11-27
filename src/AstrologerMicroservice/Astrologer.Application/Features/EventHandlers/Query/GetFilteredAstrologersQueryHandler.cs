using MediatR;
using Astrologer.Application.Features.Query;
using Astrologer.Application.Services;
using Shared.Application.Common.Contracts.Response;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using AstrologerMicroservice.Application.Service;

namespace Astrologer.Application.Features.EventHandlers.Query
{
    public class GetFilteredAstrologersQueryHandler : IRequestHandler<GetFilteredAstrologersQuery, Result>
    {
        private readonly ILoggerService<GetFilteredAstrologersQueryHandler> _logger;
        private readonly IAstrologerService _astrologerService;

        public GetFilteredAstrologersQueryHandler(
            ILoggerService<GetFilteredAstrologersQueryHandler> logger,
            IAstrologerService astrologerService)
        {
            _logger = logger;
            _astrologerService = astrologerService;
        }

        public async Task<Result> Handle(GetFilteredAstrologersQuery request, CancellationToken cancellationToken)
        {
            var response = await _astrologerService.GetFilteredAstrologersAsync(request.AttributeId,Convert.ToInt32(request.SubCategoryId),request.PageSize
            );

            if (response == null || !response.Any())
            {
                return Result.Success(new List<CatalogResponseDto>());
            }
            return Result.Success(response);

        }


    }
}