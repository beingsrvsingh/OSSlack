using MediatR;

namespace SecretManagement.Application.Features.Commands;

public class RemoveCredentialCommand : IRequest
{
    public string KeyName { get; }

    public RemoveCredentialCommand(string keyName)
    {
        KeyName = keyName;
    }
}
