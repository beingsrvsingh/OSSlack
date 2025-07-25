using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Commands;

public class RemoveCredentialCommand : IRequest<Result>
{
    public string KeyName { get; }

    public RemoveCredentialCommand(string keyName)
    {
        KeyName = keyName;
    }
}
