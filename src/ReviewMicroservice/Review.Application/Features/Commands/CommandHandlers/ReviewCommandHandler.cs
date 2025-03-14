using Mapster;
using MediatR;
using Review.Application.Services;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands.CommandHandlers
{
    public class ReviewCommandHandler : IRequestHandler<ReviewCommand, Result>
    {
        private readonly IReviewService reviewService;
        private readonly IUserProvider userProvider;

        public ReviewCommandHandler(IReviewService reviewService, IUserProvider userProvider)
        {
            this.reviewService = reviewService;
            this.userProvider = userProvider;
        }
        public Task<Result> Handle(ReviewCommand request, CancellationToken cancellationToken)
        {
            var review = request.Adapt<Review.Domain.Entities.Reviews>();
            review.UserId = userProvider.UserId;

            this.reviewService.AddAsync(review);

            return Task.FromResult(Result.Success());
        }
    }
}
