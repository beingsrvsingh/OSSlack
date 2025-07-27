using Identity.Application.Contracts;
using MediatR;
using Shared.Domain.Contracts;

namespace Identity.Application.Features.Admin.Query
{
    public class GetAllCountriesQuery : IRequest<PaginatedResult<CountryResponse>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}