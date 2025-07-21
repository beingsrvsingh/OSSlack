using MediatR;
using SecretManagement.Application.Features.Commands;
using SecretManagement.Application.Services.Interfaces;

namespace SecretManagement.Application.Features.EventHandlers.CommandHandlers;

public class RemoveCredentialCommandHandler : IRequestHandler<RemoveCredentialCommand>
{
    private readonly IPlatformManagerService _platformService;

    public RemoveCredentialCommandHandler(IPlatformManagerService platformService)
    {
        _platformService = platformService;
    }

    public Task Handle(RemoveCredentialCommand request, CancellationToken cancellationToken)
    {
        _platformService.RemoveCredential(request.KeyName);
        return Task.FromResult(Unit.Value);
    }
}
