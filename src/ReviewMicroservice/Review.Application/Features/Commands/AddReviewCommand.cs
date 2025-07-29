using MediatR;
using Review.Application.Contracts;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class AddReviewCommand : IRequest<Result>
    {
        public required int ProductId { get; set; }
        public required string UserId { get; set; }
        public int Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<MediaDto> Media { get; set; } = new();
    }

}
