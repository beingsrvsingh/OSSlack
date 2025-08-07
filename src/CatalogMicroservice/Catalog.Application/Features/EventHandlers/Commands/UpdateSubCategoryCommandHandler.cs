using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class UpdateSubCategoryCommandHandler : IRequestHandler<UpdateSubCategoryCommand, Result>
{
    private readonly ILoggerService<UpdateSubCategoryCommandHandler> _logger;
    private readonly ISubCategoryService _service;

    public UpdateSubCategoryCommandHandler(ILoggerService<UpdateSubCategoryCommandHandler> logger, ISubCategoryService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(UpdateSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateSubCategoryAsync(request.SubCategory);
        return result ? Result.Success() : Result.Failure("Failed to update subcategory.");
    }
}
}