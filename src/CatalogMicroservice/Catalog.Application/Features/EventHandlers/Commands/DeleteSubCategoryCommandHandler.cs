using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class DeleteSubCategoryCommandHandler : IRequestHandler<DeleteSubCategoryCommand, Result>
{
    private readonly ILoggerService<DeleteSubCategoryCommandHandler> _logger;
    private readonly ISubCategoryService _service;

    public DeleteSubCategoryCommandHandler(ILoggerService<DeleteSubCategoryCommandHandler> logger, ISubCategoryService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(DeleteSubCategoryCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteSubCategoryAsync(request.Id);
        return result ? Result.Success() : Result.Failure("Failed to delete subcategory.");
    }
}
}