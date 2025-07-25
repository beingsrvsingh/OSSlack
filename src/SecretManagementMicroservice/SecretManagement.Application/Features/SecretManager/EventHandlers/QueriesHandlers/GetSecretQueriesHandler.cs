using MediatR;
using Shared.Utilities.Response;
using SecretManagement.Application.Services.Interfaces;
using Shared.Application.Interfaces.Logging;

namespace SecretManagement.Application.Features.SecretManager.Queries.QueryHandler
{
    public class GetSecretQueriesHandler : IRequestHandler<GetSecretQuery, Result>
    {
        private readonly ILoggerService<GetSecretQueriesHandler> logger;
        private readonly ISecretsService iSecretsService;

        public GetSecretQueriesHandler(ILoggerService<GetSecretQueriesHandler> logger, ISecretsService iSecretsService)
        {
            this.logger = logger;
            this.iSecretsService = iSecretsService;
        }

        public async Task<Result> Handle(GetSecretQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var secret = await iSecretsService.GetSecret(
                    request.AppName,
                    request.Environment,
                    request.Key);

                if (secret == null)
                {
                    return Result.Failure(new FailureResponse(
                        "SecretNotFound",
                        $"Secret with key '{request.Key}' not found in app '{request.AppName}' and environment '{request.Environment}'."));
                }

                return Result.Success(secret);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error retrieving secret for App: {AppName}, Environment: {Environment}, Key: {Key}",
                    request.AppName, request.Environment, request.Key);

                return Result.Failure(new FailureResponse(
                    "SecretFetchFailed",
                    "An error occurred while retrieving the secret. Please try again later."));
            }
        }
    }
}

