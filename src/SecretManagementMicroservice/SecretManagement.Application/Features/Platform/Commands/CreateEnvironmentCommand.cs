
using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Commands;

public class CreateEnvironmentCommand : IRequest<Result>
{
    public string EnvironmentKey { get; set; }
    public string EnvironmentValue { get; set; }

    public CreateEnvironmentCommand(string environmentKey, string environmentValue)
    {
        EnvironmentKey = environmentKey;
        EnvironmentValue = environmentValue;
    }
}