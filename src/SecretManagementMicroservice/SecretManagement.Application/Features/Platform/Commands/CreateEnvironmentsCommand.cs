using MediatR;
using SecretManagement.Application.Features.Commands;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Platform.Commands
{
    public class CreateEnvironmentsCommand : IRequest<Result>
    {
        public List<CreateEnvironmentCommand> Credentials { get; set; } = new List<CreateEnvironmentCommand>();
    }
}