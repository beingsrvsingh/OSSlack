using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Query
{
    public class GetCategoryLocalizedTextsQueryHandler : IRequestHandler<GetCategoryLocalizedTextsQuery, Result>
    {
        private readonly ILoggerService<GetCategoryLocalizedTextsQueryHandler> _logger;
        private readonly ICategoryService _service;

        public GetCategoryLocalizedTextsQueryHandler(ILoggerService<GetCategoryLocalizedTextsQueryHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetCategoryLocalizedTextsQuery request, CancellationToken cancellationToken)
        {
            var texts = await _service.GetLocalizedTextsAsync(request.CategoryId);
            return Result.Success(texts);
        }
    }

}