using Mapster;
using MediatR;
using Review.Application.Services;
using Review.Domain.Entities;
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
            var review = request.Adapt<Reviews>();
            review.UserId = userProvider.UserId;
            review.UserName = userProvider.UserName;

            this.reviewService.AddAsync(review);

            return Task.FromResult(Result.Success());
        }
    }
}
