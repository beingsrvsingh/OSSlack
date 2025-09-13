using MediatR;
using Shared.Utilities.Response;

namespace SearchAggregator.Application.Features.Command
{
    public class AddSearchUserHistoryCommand : IRequest<Result>
    {
        public string UserId { get; set; } = null!;
        public string Query { get; set; } = null!;
        public string? Platform { get; set; }
        public string? Language { get; set; }
        public string? IPAddress { get; set; }
        public int? ResultCount { get; set; }
        public DateTime? SearchedAt { get; set; } = DateTime.UtcNow;
    }
}
