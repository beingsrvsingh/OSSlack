using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Application.Features.Commands;
using Catalog.Application.Services;
using MediatR;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.EventHandlers.Commands
{
    public class UpdatePoojaKitItemCommandHandler : IRequestHandler<UpdatePoojaKitItemCommand, Result>
{
    private readonly ILoggerService<UpdatePoojaKitItemCommandHandler> _logger;
    private readonly IPoojaKitItemService _service;

    public UpdatePoojaKitItemCommandHandler(ILoggerService<UpdatePoojaKitItemCommandHandler> logger, IPoojaKitItemService service)
    {
        _logger = logger;
        _service = service;
    }

    public async Task<Result> Handle(UpdatePoojaKitItemCommand request, CancellationToken cancellationToken)
    {
        var result = await _service.UpdateItemAsync(request.Item);
        return result ? Result.Success() : Result.Failure("Failed to update item.");
    }
}
}