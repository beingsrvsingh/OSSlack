using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Platform.Queries;

public class GetCredentialByKeyQuery : IRequest<Result>
{
    public string KeyName { get; }

    public GetCredentialByKeyQuery(string keyName)
    {
        KeyName = keyName;
    }
}
