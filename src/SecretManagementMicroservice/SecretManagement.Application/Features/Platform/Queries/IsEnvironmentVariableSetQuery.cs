
using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Queries;

public record IsEnvironmentVariableSetQuery(string Key) : IRequest<Result>;
