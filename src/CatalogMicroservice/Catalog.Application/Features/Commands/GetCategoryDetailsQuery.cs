using MediatR;
using Shared.Utilities.Response;

namespace Catalog.Application.Features.Commands
{
    public class GetCategoryDetailsQuery : IRequest<Result>
    {
        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<int> SubCategoryIds { get; set; } = new List<int>();
    }
}
