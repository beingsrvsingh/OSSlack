using MediatR;
using Shared.Utilities.Response;
using SecretManagement.Application.Features.SecretManager.Queries;
using SecretManagement.Application.Services.Interfaces;

namespace SecretManagement.Application.Features.SecretManager.Queries.QueryHandler
{
    public class GetSecretQueriesHandler : IRequestHandler<GetSecretQuery, Result>
    {
        private readonly ISecretsService iSecretsService;

        public GetSecretQueriesHandler(ISecretsService iSecretsService)
        {
            this.iSecretsService = iSecretsService;
        }

        public async Task<Result> Handle(GetSecretQuery request, CancellationToken cancellationToken)
        {        
            return Result.Success();
        }
    }
}
