using MediatR;
using Review.Application.Services;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;
using Utilities.Cryptography;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class ReviewActionCommandHandler : IRequestHandler<ReviewActionCommand, Result>
    {
        private readonly IReviewService reviewService;
        private readonly IUserProvider userProvider;

        public ReviewActionCommandHandler(IReviewService reviewService, IUserProvider userProvider)
        {
            this.reviewService = reviewService;
            this.userProvider = userProvider;
        }

        public async Task<Result> Handle(ReviewActionCommand request, CancellationToken cancellationToken)
        {
            int reviewId = Int32.Parse(Cryptography.DecryptString(request.ReviewId));
            int productId = Int32.Parse(Cryptography.DecryptString(request.ProductId));

            var records = await reviewService.GetByAsync(reviewId, productId, userProvider.UserId);

            records!.IsReviewed = true;
            records.ReviewedDate = DateTime.UtcNow;
            records.ReviewReason = request.ReviewReason;
            records.ReviewedBy = userProvider.UserName;

            await this.reviewService.UpdateAsync(records);

            return Result.Success();
        }
    }
}
