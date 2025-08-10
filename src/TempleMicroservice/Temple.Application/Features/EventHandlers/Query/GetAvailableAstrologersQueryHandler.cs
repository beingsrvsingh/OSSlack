using Temple.Application.Features.Query;
using Temple.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Temple.Application.Features.EventHandlers.Query
{
    public class GetAvailableAstrologersQueryHandler : IRequestHandler<GetAvailableAstrologersQuery, Result>
    {
        private readonly ITempleService _astrologerService;
        private readonly ILoggerService<GetAvailableAstrologersQueryHandler> _logger;

        public GetAvailableAstrologersQueryHandler(ILoggerService<GetAvailableAstrologersQueryHandler> logger,
        ITempleService astrologerService)
        {
            _astrologerService = astrologerService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetAvailableAstrologersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching available astrologers for Date: {Date}, Language: {Language}, Expertise: {Expertise}",
         request.Date, request.Language, request.Expertise);

            var astrologers = await _astrologerService.GetAvailableAsync(request.Date, nameof(request.Language), nameof(request.Expertise));

            if (astrologers != null && astrologers.Any())
            {
                return Result.Success(astrologers);
            }
            else
            {
                _logger.LogWarning("No available astrologers found for Date: {Date}, Language: {Language}, Expertise: {Expertise}",
                    request.Date, request.Language, request.Expertise);

                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "No available astrologers found matching the criteria."));
            }
        }
    }

}