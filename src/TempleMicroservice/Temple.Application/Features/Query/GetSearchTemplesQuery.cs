using Temple.Domain.Entities.Enums;
using MediatR;
using Shared.Utilities.Response;

namespace Temple.Application.Features.Query
{
    public class GetSearchTemplesQuery : IRequest<Result>
    {
        public string? Language { get; set; }
        public string? Expertise { get; set; }
        public ConsultationMode? ConsultationMode { get; set; }
        public bool? IsActive { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}