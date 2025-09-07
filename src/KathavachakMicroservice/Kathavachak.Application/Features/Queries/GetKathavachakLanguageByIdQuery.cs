using MediatR;
using Shared.Utilities.Response;

namespace Kathavachak.Application.Features.Queries
{
    public class GetKathavachakLanguageByIdQuery : IRequest<Result>
    {
        public int Id { get; set; }
        public GetKathavachakLanguageByIdQuery(int id) => Id = id;
    }
}
