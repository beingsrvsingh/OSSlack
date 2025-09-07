using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;
using Temple.Application.Features.Queries;
using Temple.Application.Services;

namespace Temple.Application.Features.EventHandlers.Queries
{
    public class GetTempleDonationByIdQueryHandler : IRequestHandler<GetTempleDonationByIdQuery, Result>
    {
        private readonly ITempleDonationService _service;
        private readonly ILoggerService<GetTempleDonationByIdQueryHandler> _logger;

        public GetTempleDonationByIdQueryHandler(ITempleDonationService service, ILoggerService<GetTempleDonationByIdQueryHandler> logger)
        {
            _service = service;
            _logger = logger;
        }

        public async Task<Result> Handle(GetTempleDonationByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInfo("Fetching temple donation with ID: {Id}", request.Id);

            var entity = await _service.GetByIdAsync(request.Id);

            if (entity != null)
                return Result.Success(entity);

            _logger.LogWarning("Temple donation not found for ID: {Id}", request.Id);
            return Result.Failure(new FailureResponse("DONATION_NOT_FOUND", "Temple donation not found."));
        }
    }

}
