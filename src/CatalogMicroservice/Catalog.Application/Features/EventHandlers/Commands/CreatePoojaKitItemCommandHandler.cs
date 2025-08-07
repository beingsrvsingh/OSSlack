using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class CreatePoojaKitItemCommandHandler : IRequestHandler<CreatePoojaKitItemCommand, Result>
{
    private readonly ILoggerService<CreatePoojaKitItemCommandHandler> _logger;
    private readonly IPoojaKitItemService _service;

    public CreatePoojaKitItemCommandHandler(ILoggerService<CreatePoojaKitItemCommandHandler> logger, IPoojaKitItemService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(CreatePoojaKitItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.CreateItemAsync(request.Item);
        return result ? Result.Success() : Result.Failure("Failed to create item.");
    }
}
}