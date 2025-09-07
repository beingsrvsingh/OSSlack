using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTemplePoojaByIdQueryHandler : IRequestHandler<GetTemplePoojaByIdQuery, Result>
    {
        private readonly ITemplePoojaService _poojaService;
        private readonly ILoggerService<GetTemplePoojaByIdQueryHandler> _logger;

        public GetTemplePoojaByIdQueryHandler(
            ITemplePoojaService poojaService,
            ILoggerService<GetTemplePoojaByIdQueryHandler> logger)
        {
            _poojaService = poojaService;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTemplePoojaByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching pooja with ID: {Id}", request.Id);

            var pooja = await _poojaService.GetByIdAsync(request.Id);

            if (pooja != null)
            {
                return Result.Success(pooja);
            }
            else
            {
                _logger.LogWarning("Pooja not found for ID: {Id}", request.Id);
                return Result.Failure(new FailureResponse("POOJA_NOT_FOUND", "Pooja not found."));
            }
        }
    }

}
