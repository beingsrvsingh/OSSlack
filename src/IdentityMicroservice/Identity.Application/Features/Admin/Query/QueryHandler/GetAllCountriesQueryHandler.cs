using Identity.Application.Contracts;
using Identity.Application.Services.Interfaces;
using MediatR;
using Shared.Domain.Contracts;

namespace Identity.Application.Features.Admin.Query.QueryHandler
{
    public class GetAllCountriesQueryHandler : IRequestHandler<GetAllCountriesQuery, PaginatedResult<CountryResponse>>
    {
        private readonly IAdminService adminService;

        public GetAllCountriesQueryHandler(IAdminService adminService)
        {
            this.adminService = adminService;
        }

        public async Task<PaginatedResult<CountryResponse>> Handle(GetAllCountriesQuery request, CancellationToken cancellationToken)
        {
            var (items, totalCount) = await adminService.GetCountriesAsync(
            searchTerm: null,
            sortBy: "Name",
            isDescending: false,
            pageNumber: request.PageNumber,
            pageSize: request.PageSize,
            cancellationToken: cancellationToken);

            return new PaginatedResult<CountryResponse>(items, totalCount, request.PageNumber, request.PageSize);
        }
    }

}