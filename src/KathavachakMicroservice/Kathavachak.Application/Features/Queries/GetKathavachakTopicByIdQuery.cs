using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakTopicByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakTopicByIdQuery(int id) => Id = id;
    }
}
