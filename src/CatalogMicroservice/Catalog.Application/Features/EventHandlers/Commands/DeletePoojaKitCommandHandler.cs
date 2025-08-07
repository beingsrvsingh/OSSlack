using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class DeletePoojaKitCommandHandler : IRequestHandler<DeletePoojaKitCommand, Result>
{
    private readonly ILoggerService<DeletePoojaKitCommandHandler> _logger;
    private readonly IPoojaKitService _service;

    public DeletePoojaKitCommandHandler(ILoggerService<DeletePoojaKitCommandHandler> logger, IPoojaKitService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(DeletePoojaKitCommand request, CancellationToken cancellationToken)
    {
        var success = await _service.DeletePoojaKitAsync(request.Id);
        if (!success)
        {
            return Result.Failure(new FailureResponse("Error", $"Failed to delete pooja kit with id {request.Id}."));
        }
        return Result.Success(true);
    }
}
}