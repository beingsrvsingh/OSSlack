using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Commands;

public record RemoveEnvironmentVariableCommand(string Key) : IRequest<Result>;
