using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewCommand:IRequest<Result>
    {
        public required string ProductId { get; set; }
        public int Star { get; set; } = 0;
        public required string Title { get; set; }
        public string ShortDescription { get; set; } = null!;

    }
}
