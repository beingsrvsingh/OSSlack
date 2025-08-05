using MediatR;
using Shared.Utilities.Response;

namespace AstrologerMicroservice.Application.Features.Query
{
    public record GetAstrologerByIdQuery(int Id) : IRequest<Result>;

}