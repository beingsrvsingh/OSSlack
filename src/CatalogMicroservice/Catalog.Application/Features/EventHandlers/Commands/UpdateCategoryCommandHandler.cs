using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Result>
    {
        private readonly ILoggerService<UpdateCategoryCommandHandler> _logger;
        private readonly ICategoryService _service;

        public UpdateCategoryCommandHandler(ILoggerService<UpdateCategoryCommandHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.UpdateCategoryAsync(request.Category);
            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Failed to update category"));
        }
    }

}