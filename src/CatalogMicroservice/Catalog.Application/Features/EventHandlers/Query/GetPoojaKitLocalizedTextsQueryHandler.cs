using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetPoojaKitLocalizedTextsQueryHandler : IRequestHandler<GetPoojaKitLocalizedTextsQuery, Result>
    {
        private readonly ILoggerService<GetPoojaKitLocalizedTextsQueryHandler> _logger;
        private readonly IPoojaKitService _service;

        public GetPoojaKitLocalizedTextsQueryHandler(ILoggerService<GetPoojaKitLocalizedTextsQueryHandler> logger, IPoojaKitService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetPoojaKitLocalizedTextsQuery request, CancellationToken cancellationToken)
        {
            var texts = await _service.GetLocalizedTextsAsync(request.KitId);
            if (texts == null || !texts.Any())
            {
                return Result.Failure(new FailureResponse("NotFound", "No localized texts found."));
            }
            return Result.Success(texts);
        }
    }
}