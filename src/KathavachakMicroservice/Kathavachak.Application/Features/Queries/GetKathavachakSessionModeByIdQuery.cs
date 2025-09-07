using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakSessionModeByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakSessionModeByIdQuery(int id) => Id = id;
    }
}
