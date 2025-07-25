using MediatR;
using SecretManagement.Application.Queries;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.EventHandlers.QueriesHandlers;

public class GetAllCredentialsQueryHandler : IRequestHandler<GetAllCredentialsQuery, Result>
{
    private readonly ILoggerService<GetAllCredentialsQueryHandler> logger;
    private readonly IPlatformManagerService _platformService;

    public GetAllCredentialsQueryHandler(ILoggerService<GetAllCredentialsQueryHandler> logger, IPlatformManagerService platformService)
    {
        this.logger = logger;
        _platformService = platformService;
    }

    public async Task<Result> Handle(GetAllCredentialsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var keys = await _platformService.GetAllCredentialKeys();
            return Result.Success(keys);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An exception occurred while retrieving all credentials.");
            
            return Result.Failure(new FailureResponse(
                    "CredentialFetchFailed",
                    "An error occurred while retrieving credentials. Please try again later."
                ));
        }
    }
}
