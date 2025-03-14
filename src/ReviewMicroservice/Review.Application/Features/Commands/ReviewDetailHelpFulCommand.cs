using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewDetailHelpFulCommand : IRequest<Result>
    {
        public required string ReviewId { get; set; }
        public required string ProductId { get; set; }
        public bool Helpful { get; set; }
    }
}
