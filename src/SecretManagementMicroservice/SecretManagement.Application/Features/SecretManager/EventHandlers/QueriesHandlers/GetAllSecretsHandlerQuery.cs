
using MediatR;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.SecretManager.Queries.QueryHandler;
public class GetAllSecretsHandlerQueryHandler : IRequestHandler<GetAllSecretsQuery, Result>
{
    private readonly ILoggerService<GetAllSecretsHandlerQueryHandler> logger;
    private readonly ISecretsService _secretsService;

    public GetAllSecretsHandlerQueryHandler(ILoggerService<GetAllSecretsHandlerQueryHandler> logger, ISecretsService secretsService)
    {
        this.logger = logger;
        _secretsService = secretsService;
    }

    public async Task<Result> Handle(GetAllSecretsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var secrets = await _secretsService.GetAllSecrets(request.UserId);

            return Result.Success(secrets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while retrieving secrets.");

            return Result.Failure(new FailureResponse(
                "SecretFetchFailed",
                "An error occurred while retrieving secrets. Please try again later."
            ));
        }
    }
}