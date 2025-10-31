using MediatR;
using Shared.Utilities.Response;

namespace Pooja.Application.Features.Queries
{
    public class SearchPoojasQuery : IRequest<Result>
    {
        public string Keyword { get; set; } = string.Empty;
    }

}
