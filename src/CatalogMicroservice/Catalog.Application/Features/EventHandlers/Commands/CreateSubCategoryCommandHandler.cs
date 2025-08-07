using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class CreateSubCategoryCommandHandler : IRequestHandler<CreateSubCategoryCommand, Result>
    {
        private readonly ILoggerService<CreateSubCategoryCommandHandler> _logger;
        private readonly ISubCategoryService _service;

        public CreateSubCategoryCommandHandler(ILoggerService<CreateSubCategoryCommandHandler> logger, ISubCategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(CreateSubCategoryCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.CreateSubCategoryAsync(request.SubCategory);
            return result ? Result.Success() : Result.Failure("Failed to create subcategory.");
        }
    }
}