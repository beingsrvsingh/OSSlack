using Mapster;
using MediatR;
using Review.Application.Services;
using Review.Domain.Entities;
using Shared.Application.Interfaces;
using Shared.Utilities.Response;
using Utilities.Cryptography;

namespace Review.Application.Features.Commands
{
    public class ReviewDetailHelpFulCommandHandler : IRequestHandler<ReviewDetailHelpFulCommand, Result>
    {
        private readonly IReviewDetailService reviewService;
        private readonly IUserProvider userProvider;

        public ReviewDetailHelpFulCommandHandler(IReviewDetailService reviewService, IUserProvider userProvider)
        {
            this.reviewService = reviewService;
            this.userProvider = userProvider;
        }
        public async Task<Result> Handle(ReviewDetailHelpFulCommand request, CancellationToken cancellationToken)
        {
            int reviewId = Int32.Parse(Cryptography.DecryptString(request.ReviewId));
            int productId = Int32.Parse(Cryptography.DecryptString(request.ProductId));

            var records = await reviewService.GetByAsync(reviewId, productId, userProvider.UserId);

            if (records is null)
            {
                var review = request.Adapt<ReviewDetail>();
                review.UserId = userProvider.UserId;

                await this.reviewService.AddAsync(review);
            }
            else
            {
                records.IsHelpful = request.Helpful;
                records.ModifiedDate = DateTime.UtcNow;

                await this.reviewService.UpdateAsync(records);
            }

            return Result.Success();
        }
    }
}
