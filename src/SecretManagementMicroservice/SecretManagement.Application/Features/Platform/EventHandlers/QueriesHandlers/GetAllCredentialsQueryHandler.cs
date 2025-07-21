using MediatR;
using SecretManagement.Application.Queries;
using SecretManagement.Application.Services.Interfaces;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class GetAllCredentialsQueryHandler : IRequestHandler<GetAllCredentialsQuery, Result>
{
    private readonly IPlatformManagerService _platformService;

    public GetAllCredentialsQueryHandler(IPlatformManagerService platformService)
    {
        _platformService = platformService;
    }

    public Task<Result> Handle(GetAllCredentialsQuery request, CancellationToken cancellationToken)
    {
        var keys = _platformService.GetAllCredentialKeys();
        return Task.FromResult(result: Result.Success(keys));
    }
}
