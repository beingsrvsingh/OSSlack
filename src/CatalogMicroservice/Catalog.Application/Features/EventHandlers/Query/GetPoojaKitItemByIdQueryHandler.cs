using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetPoojaKitItemByIdQueryHandler : IRequestHandler<GetPoojaKitItemByIdQuery, Result>
    {
        private readonly ILoggerService<GetPoojaKitItemByIdQueryHandler> _logger;
        private readonly IPoojaKitItemService _service;

        public GetPoojaKitItemByIdQueryHandler(ILoggerService<GetPoojaKitItemByIdQueryHandler> logger, IPoojaKitItemService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetPoojaKitItemByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetItemByIdAsync(request.Id);
            return result is not null ? Result.Success(result) : Result.Failure("Item not found.");
        }
    }
}