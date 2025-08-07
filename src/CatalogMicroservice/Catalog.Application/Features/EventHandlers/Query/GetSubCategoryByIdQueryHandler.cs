using Catalog.Application.Features.Query;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class GetSubCategoryByIdQueryHandler : IRequestHandler<GetSubCategoryByIdQuery, Result>
    {
        private readonly ILoggerService<GetSubCategoryByIdQueryHandler> _logger;
        private readonly ISubCategoryService _service;

        public GetSubCategoryByIdQueryHandler(ILoggerService<GetSubCategoryByIdQueryHandler> logger, ISubCategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(GetSubCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var subCategory = await _service.GetSubCategoryByIdAsync(request.Id);
            return subCategory is not null ? Result.Success(subCategory) : Result.Failure("Subcategory not found.");
        }
    }
}