using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakCategoryByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakCategoryByIdQuery(int id) => Id = id;
    }
}
