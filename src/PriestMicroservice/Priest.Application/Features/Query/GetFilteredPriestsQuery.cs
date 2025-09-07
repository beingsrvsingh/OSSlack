using MediatR;
using Shared.Utilities.Response;


namespace Priest.Application.Features.Query
{
    public class GetFilteredPriestsQuery : IRequest<Result>
    {
        public string? Language { get; set; }
        public string? Expertise { get; set; }
    }

}
