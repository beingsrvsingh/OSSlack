using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Query
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Result>
    {
        private readonly ILoggerService<DeleteCategoryCommandHandler> _logger;
        private readonly ICategoryService _service;

        public DeleteCategoryCommandHandler(ILoggerService<DeleteCategoryCommandHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.DeleteCategoryAsync(request.Id);
            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Failed to delete category"));
        }
    }

}