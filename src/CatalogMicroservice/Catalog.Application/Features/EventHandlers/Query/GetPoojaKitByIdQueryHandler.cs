using Catalog.Application.Features.Queries.QueryHandlers;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetPoojaKitByIdQueryHandler : IRequestHandler<GetPoojaKitByIdQuery, Result>
    {
        private readonly ILoggerService<GetPoojaKitByIdQueryHandler> _logger;
        private readonly IPoojaKitService _service;

        public GetPoojaKitByIdQueryHandler(ILoggerService<GetPoojaKitByIdQueryHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetPoojaKitByIdQuery request, CancellationToken cancellationToken)
        {
            var kit = await _service.GetPoojaKitByIdAsync(request.Id);
            if (kit == null)
            {
                return Result.Failure(new FailureResponse("NotFound", $"Pooja kit with id {request.Id} not found."));
            }
            return Result.Success(kit);
        }
    }
}