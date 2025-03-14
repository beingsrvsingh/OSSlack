﻿using MediatR;
using Review.Application.Contracts;
using Review.Application.Services;
using Review.Domain.Entities;
using Shared.Application.Common.Services.Interfaces;
using Shared.Utilities.Response;
using Utilities.Cryptography;

namespace Review.Application.Features.Queries.QueryHandlers
{
    public class GetReviewByProductQueryHandler : IRequestHandler<GetReviewByProductQuery, Result>
    {
        private readonly IReviewService reviewService;
        private readonly ILoggerService logger;
        private readonly HttpClient httpClient;
        private readonly IIdentityAPIClient identityAPIClient;
        private readonly IUserProvider userProvider;

        public GetReviewByProductQueryHandler(IReviewService reviewService, ILoggerService logger, HttpClient httpClient, 
            IIdentityAPIClient identityAPIClient, IUserProvider userProvider)
        {
            this.reviewService = reviewService;
            this.logger = logger;
            this.httpClient = httpClient;
            this.identityAPIClient = identityAPIClient;
            this.userProvider = userProvider;
        }
        public async Task<Result> Handle(GetReviewByProductQuery request, CancellationToken cancellationToken)
        {
            var result = await this.reviewService.GetReviewByProduct(Int32.Parse(Cryptography.DecryptString(request.ProductId)));

            if (!result.Any())
            {
                string message = "No Records Found.";
                logger.LogInfo(message);
                return Result.Success(message);
            }

            var userIds = new { UserId = result.Select(x => x.UserId).ToArray()!, };

            var response = await identityAPIClient.GetUserAsync(userIds);

            if (response == null)
                logger.LogInfo("No Users Founds in IdentityMicroservice.");

            List<ReviewResponse> reviews = PrepareReviews(result, response);

            return Result.Success(reviews);
        }

        private static List<ReviewResponse> PrepareReviews(IReadOnlyList<Reviews> result, GetUserReponse? response)
        {
            List<ReviewResponse> reviews = new();

            foreach (var item in result)
            {
                string name = !response!.Succeeded ? "Annoymous" : response.Data.FirstOrDefault(r => r.UserId == item.UserId)!.UserName;

                var reviewDetail = item.ReviewDetails.FirstOrDefault(x => (x.ReviewId == item.Id) && (x.UserId == item.UserId));

                bool isHelpFul = reviewDetail is null ? false : reviewDetail.IsHelpFul;
                bool isReported = reviewDetail is null ? false : (reviewDetail.ReviewReportLookUpid > 0 ? true : false);

                ReviewResponse review = new()
                {
                    Id = Cryptography.EncryptString(item.Id.ToString()),
                    ProductId = Cryptography.EncryptString(item.ProductId.ToString()!),
                    UserName = name,
                    Star = item.Star,
                    Title = item.Title,
                    Description = item.ShortDescription,
                    CreatedDate = item.CreatedDate,
                    IsHelpFulMarked = isHelpFul,
                    IsReported = isReported,
                };

                reviews.Add(review);
            }

            return reviews;
        }
    }
}
