using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Result>
    {
        private readonly ILoggerService<CreateCategoryCommandHandler> _logger;
        private readonly ICategoryService _service;

        public CreateCategoryCommandHandler(ILoggerService<CreateCategoryCommandHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.CreateCategoryAsync(request.Category);
            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Failed to create category"));
        }
    }

}