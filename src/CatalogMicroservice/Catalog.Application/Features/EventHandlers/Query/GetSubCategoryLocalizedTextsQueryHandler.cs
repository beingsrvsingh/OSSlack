using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetSubCategoryLocalizedTextsQueryHandler : IRequestHandler<GetSubCategoryLocalizedTextsQuery, Result>
    {
        private readonly ILoggerService<GetSubCategoryLocalizedTextsQueryHandler> _logger;
        private readonly ISubCategoryService _service;

        public GetSubCategoryLocalizedTextsQueryHandler(ILoggerService<GetSubCategoryLocalizedTextsQueryHandler> logger, ISubCategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetSubCategoryLocalizedTextsQuery request, CancellationToken cancellationToken)
        {
            var result = await _service.GetLocalizedTextsAsync(request.SubCategoryId);
            return result.Any() ? Result.Success(result) : Result.Failure("No localized texts found.");
        }
    }
}