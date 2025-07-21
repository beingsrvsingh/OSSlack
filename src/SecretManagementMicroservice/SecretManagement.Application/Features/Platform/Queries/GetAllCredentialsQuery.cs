using MediatR;
using Shared.Utilities.Response;

namespace SecretManagement.Application.Queries;

public class GetAllCredentialsQuery : IRequest<Result>
{
}
