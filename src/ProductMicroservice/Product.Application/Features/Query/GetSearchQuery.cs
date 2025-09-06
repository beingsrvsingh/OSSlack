using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Utilities.Response;

namespace Product.Application.Features.Query
{
    public class GetSearchQuery : IRequest<Result>
    {
        [FromQuery(Name = "q")]
        public string Query { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
