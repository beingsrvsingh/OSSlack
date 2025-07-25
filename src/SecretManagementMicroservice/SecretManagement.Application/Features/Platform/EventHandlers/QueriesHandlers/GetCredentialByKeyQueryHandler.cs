using MediatR;
using SecretManagement.Application.Platform.Queries;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class GetCredentialByKeyQueryHandler : IRequestHandler<GetCredentialByKeyQuery, Result>
{
    private readonly ILoggerService<GetCredentialByKeyQuery> logger;
    private readonly IPlatformManagerService _platformService;

    public GetCredentialByKeyQueryHandler(ILoggerService<GetCredentialByKeyQuery> logger, IPlatformManagerService platformService)
    {
        this.logger = logger;
        _platformService = platformService;
    }

    public async Task<Result> Handle(GetCredentialByKeyQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var secret = await _platformService.GetCredential(request.KeyName);

            if (string.IsNullOrEmpty(secret))
            {
                return Result.Failure(new FailureResponse("NotFound", "Credential not found."));
            }

            return Result.Success(secret);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "An error occurred while getting credential by key.");
            return Result.Failure(new FailureResponse("CredentialFetchFailed", "An error occurred while retrieving the credential. Please try again later."));

        }
    }
}
