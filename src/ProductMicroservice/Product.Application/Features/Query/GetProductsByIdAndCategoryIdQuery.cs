
using MediatR;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetProductsByIdAndCategoryIdQuery : IRequest<Result>
    {
        public int Cid { get; set; }
        public List<int> Pids { get; set; } = new();
    }
}