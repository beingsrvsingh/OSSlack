using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Features.Queries;

public record GetEnvironmentVariableQuery(string Key) : IRequest<Result>;
