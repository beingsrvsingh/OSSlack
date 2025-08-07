using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class AddOrUpdateSubCategoryLocalizedTextCommandHandler : IRequestHandler<AddOrUpdateSubCategoryLocalizedTextCommand, Result>
    {
        private readonly ILoggerService<AddOrUpdateSubCategoryLocalizedTextCommandHandler> _logger;
        private readonly ISubCategoryService _service;

        public AddOrUpdateSubCategoryLocalizedTextCommandHandler(ILoggerService<AddOrUpdateSubCategoryLocalizedTextCommandHandler> logger, ISubCategoryService service)
        {
            _logger = logger;
            _service = service;
        }

        public async Task<Result> Handle(AddOrUpdateSubCategoryLocalizedTextCommand request, CancellationToken cancellationToken)
        {
            var result = await _service.AddOrUpdateLocalizedTextAsync(request.LocalizedText);
            return result ? Result.Success() : Result.Failure("Failed to add/update localized text.");
        }
    }
}