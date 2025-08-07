using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetItemsByKitIdQueryHandler : IRequestHandler<GetItemsByKitIdQuery, Result>
    {
        private readonly ILoggerService<GetItemsByKitIdQueryHandler> _logger;
        private readonly IPoojaKitItemService _service;

        public GetItemsByKitIdQueryHandler(ILoggerService<GetItemsByKitIdQueryHandler> logger, IPoojaKitItemService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetItemsByKitIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetItemsByKitIdAsync(request.PoojaKitId);
            return result.Any() ? Result.Success(result) : Result.Failure("No items found.");
        }
    }

}