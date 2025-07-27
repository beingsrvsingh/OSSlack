
using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Commands;

public record SetEnvironmentVariableCommand(string Key, string? Value) : IRequest<Result>;
