using MediatR;

namespace SecretManagement.Application.Platform.Queries;

public class GetCredentialByKeyQuery : IRequest<string?>
{
    public string KeyName { get; }

    public GetCredentialByKeyQuery(string keyName)
    {
        KeyName = keyName;
    }
}
