using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.SecretManager.Queries;

public class GetAllSecretsQuery : IRequest<Result>
{
    public String UserId { get; init; }

    public GetAllSecretsQuery(String userId)
    {
        this.UserId = userId;
    }

}