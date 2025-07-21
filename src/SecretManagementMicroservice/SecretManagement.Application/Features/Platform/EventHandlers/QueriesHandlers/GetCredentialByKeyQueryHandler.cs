using MediatR;
using SecretManagement.Application.Platform.Queries;
using SecretManagement.Application.Services.Interfaces;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class GetCredentialByKeyQueryHandler : IRequestHandler<GetCredentialByKeyQuery, string?>
{
    private readonly IPlatformManagerService _platformService;

    public GetCredentialByKeyQueryHandler(IPlatformManagerService platformService)
    {
        _platformService = platformService;
    }

    public Task<string?> Handle(GetCredentialByKeyQuery request, CancellationToken cancellationToken)
    {
        var secret = _platformService.GetCredential(request.KeyName);
        return Task.FromResult(secret);
    }
}
