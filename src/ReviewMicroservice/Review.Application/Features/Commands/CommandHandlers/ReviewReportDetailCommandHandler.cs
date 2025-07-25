using Mapster;
using MediatR;
using Review.Application.Services;
using Review.Domain.Entities;
using Shared.Application.Interfaces;
using Shared.Utilities.Cryptography;
using Shared.Utilities.Response;

namespace Review.Application.Features.Commands
{
    public class ReviewReportDetailCommandHandler : IRequestHandler<ReviewReportDetailCommand, Result>
    {
        private readonly IReviewDetailService reportService;
        private readonly IUserProvider userProvider;

        public ReviewReportDetailCommandHandler(IReviewDetailService reportService, IUserProvider userProvider)
        {
            this.reportService = reportService;
            this.userProvider = userProvider;
        }
        public async Task<Result> Handle(ReviewReportDetailCommand request, CancellationToken cancellationToken)
        {
            int reviewId = Int32.Parse(Cryptography.DecryptString(request.ReviewId));
            int productId = Int32.Parse(Cryptography.DecryptString(request.ProductId));
            int reportLookupId = Int32.Parse(Cryptography.DecryptString(request.ReportId));

            var records = await reportService.GetByAsync(reviewId, productId, userProvider.UserId);

            if (records is null)
            {
                var review = request.Adapt<ReviewDetail>();
                review.UserId = userProvider.UserId;
                review.ReviewId = reviewId;
                review.ProductId = productId;
                records!.ReviewReportLookUpId = reportLookupId;
                review.Message = request.Message;                

                await this.reportService.AddAsync(review);
            }
            else
            {
                //Lookup id
                records.ReviewReportLookUpId = reportLookupId;
                records.Message = request.Message;
                records.ModifiedDate = DateTime.UtcNow;

                await this.reportService.UpdateAsync(records);
            }
            return Result.Success();
        }
    }
}
