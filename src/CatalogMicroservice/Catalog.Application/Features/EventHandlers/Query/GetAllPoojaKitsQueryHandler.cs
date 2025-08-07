using Catalog.Application.Features.Queries;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetAllPoojaKitsQueryHandler : IRequestHandler<GetAllPoojaKitsQuery, Result>
    {
        private readonly ILoggerService<GetAllPoojaKitsQueryHandler> _logger;
        private readonly IPoojaKitService _service;

        public GetAllPoojaKitsQueryHandler(ILoggerService<GetAllPoojaKitsQueryHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetAllPoojaKitsQuery request, CancellationToken cancellationToken)
        {
            var kits = await _service.GetAllPoojaKitsAsync();
            if (kits == null || !kits.Any())
            {
                return Result.Failure(new FailureResponse("NotFound", "No pooja kits found."));
            }
            return Result.Success(kits);
        }
    }

}