using AstrologerMicroservice.Application.Features.Query;
using AstrologerMicroservice.Application.Service;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.EventHandlers.Query
{
    public class GetSearchAstrologersQueryHandler : IRequestHandler<GetSearchAstrologersQuery, Result>
    {
        private readonly ILoggerService<GetSearchAstrologersQueryHandler> _logger;
        private readonly IAstrologerService astrolgoerService;

        public GetSearchAstrologersQueryHandler(ILoggerService<GetSearchAstrologersQueryHandler> logger, IAstrologerService astrolgoerService)
        {
            this._logger = logger;
            this.astrolgoerService = astrolgoerService;
        }

        public async Task<Result> Handle(GetSearchAstrologersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Searching astrologers with filters - Language: {Language}, Expertise: {Expertise}, ConsultationMode: {ConsultationMode}, IsActive: {IsActive}, Page: {Page}, PageSize: {PageSize}",
            request.Language!, request.Expertise!, request.ConsultationMode!, request.IsActive!, request.Page, request.PageSize);


            var astrologers = await astrolgoerService.SearchAsync(
                language: request.Language,
                expertise: request.Expertise,
                consultationMode: request.ConsultationMode,
                isActive: request.IsActive,
                page: request.Page,
                pageSize: request.PageSize);

            if (astrologers != null && astrologers.Any())
            {
                return Result.Success(astrologers);
            }
            else
            {
                _logger.LogWarning("No astrologers found matching the search criteria.");
                return Result.Failure(new FailureResponse("ASTRO_NOT_FOUND", "No astrologers found matching the search criteria."));
            }
        }
    }
}