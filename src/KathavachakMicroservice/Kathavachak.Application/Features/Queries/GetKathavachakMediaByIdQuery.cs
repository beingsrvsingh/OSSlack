using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakMediaByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakMediaByIdQuery(int id) => Id = id;
    }
}
