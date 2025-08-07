using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class AddOrUpdateCategoryLocalizedTextCommandHandler : IRequestHandler<AddOrUpdateCategoryLocalizedTextCommand, Result>
    {
        private readonly ILoggerService<AddOrUpdateCategoryLocalizedTextCommandHandler> _logger;
        private readonly ICategoryService _service;

        public AddOrUpdateCategoryLocalizedTextCommandHandler(ILoggerService<AddOrUpdateCategoryLocalizedTextCommandHandler> logger, ICategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(AddOrUpdateCategoryLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            var success = await _service.AddOrUpdateLocalizedTextAsync(request.LocalizedText);
            return success
                ? Result.Success()
                : Result.Failure(new FailureResponse("Failed", "Failed to update localized text"));
        }
    }

}