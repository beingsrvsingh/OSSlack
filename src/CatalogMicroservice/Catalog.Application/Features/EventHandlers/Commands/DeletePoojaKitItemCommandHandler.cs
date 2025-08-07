using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class DeletePoojaKitItemCommandHandler : IRequestHandler<DeletePoojaKitItemCommand, Result>
{
    private readonly ILoggerService<DeletePoojaKitItemCommandHandler> _logger;
    private readonly IPoojaKitItemService _service;

    public DeletePoojaKitItemCommandHandler(ILoggerService<DeletePoojaKitItemCommandHandler> logger, IPoojaKitItemService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(DeletePoojaKitItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.DeleteItemAsync(request.Id);
        return result ? Result.Success() : Result.Failure("Failed to delete item.");
    }
}
}