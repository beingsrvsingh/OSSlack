using MediatR;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewFeedbackCommand : IRequest<Result>
    {
        public required int ReviewId { get; set; }
        public required string UserId { get; set; }
        public bool IsHelpful { get; set; }
    }
}