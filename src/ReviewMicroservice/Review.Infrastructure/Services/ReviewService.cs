using Mapster;
using Microsoft.EntityFrameworkCore;
using Review.Application.Contracts;
using Review.Application.Features.Commands;
using Review.Application.Features.Queries;
using Review.Application.Services;
using Review.Domain.Core.Repository;
using Review.Domain.Entities;
using Review.Domain.Enum;
using Review.Domain.Repository;
using Shared.Application.Interfaces;
using Shared.Application.Interfaces.Logging;

using ReviewEntity = Review.Domain.Entities.Review;

namespace Review.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly IReviewReportRepository _reportRepository;
        private readonly IReviewReportReasonRepository _reviewReportReasonRepository;
        private readonly IReviewFeedbackRepository _reviewFeedbackRepository;
        private readonly IReviewMediaRepository _mediaRepository;
        private readonly IUserProvider userProvider;
        private readonly ILoggerService<ReviewService> _logger;

        public ReviewService(
            ILoggerService<ReviewService> logger,
            IReviewRepository reviewRepository,
            IReviewReportRepository reportRepository,
            IReviewReportReasonRepository reviewReportReasonRepository,
            IReviewFeedbackRepository reviewFeedbackRepository,
            IReviewMediaRepository mediaRepository,
            IUserProvider userProvider
            )
        {
            _reviewRepository = reviewRepository;
            _reportRepository = reportRepository;
            _reviewReportReasonRepository = reviewReportReasonRepository;
            _reviewFeedbackRepository = reviewFeedbackRepository;
            this._mediaRepository = mediaRepository;
            this.userProvider = userProvider;
            _logger = logger;
        }

        public async Task<List<ReviewReportReasonDto>> GetReportReasonsAsync()
        {
            var reasons = await _reviewReportReasonRepository.GetAllByDisplayOrderAsync();
            return reasons.Adapt<List<ReviewReportReasonDto>>();
        }

        public async Task<ReviewEntity?> GetEntityByIdAsync(int reviewId)
        {
            var review = await _reviewRepository.GetByIdAsync(reviewId);
            if (review == null)
                _logger.LogWarning("Review with ID {ReviewId} not found", reviewId);
            else
                _logger.LogInfo("Fetched review with ID {ReviewId}", reviewId);

            return review;
        }

        public async Task<ReviewDto?> GetByReviewIdAsync(int reviewId)
        {
            var review = await GetEntityByIdAsync(reviewId);
            return review?.Adapt<ReviewDto>();
        }

        public async Task<bool> AddReviewAsync(AddReviewCommand command)
        {
            try
            {
                var existingReview = await _reviewRepository.GetByUserAndProductAsync(command.UserId, command.ProductId);
                if (existingReview != null)
                {
                    _logger.LogError("Review already exists for product {ProductId} by user {UserId}", command.ProductId, command.UserId);
                    return false;
                }

                var review = command.Adapt<ReviewEntity>();
                review.CreatedAt = DateTime.UtcNow;
                review.Status = ReviewStatus.Active;

                review.Medias = command.Media?.Select(m => new ReviewMedia
                {
                    Url = m.Url,
                    Type = m.Type
                }).ToList() ?? new List<ReviewMedia>();

                await _reviewRepository.AddAsync(review);
                await _reviewRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding review for product {ProductId} by user {UserId}", command.ProductId, command.UserId);
                return false;
            }
        }

        public async Task<bool> UpdateReviewAsync(UpdateReviewCommand command)
        {
            try
            {
                var review = await GetEntityByIdAsync(command.ReviewId);
                if (review == null)
                    return false;

                // Manually update fields or use mapping profiles for safety
                command.Adapt(review);
                review.UpdatedAt = DateTime.UtcNow;

                var existingMedia = await _mediaRepository.GetByReviewIdAsync(command.ReviewId);
                if (existingMedia.Any())
                {
                    await _mediaRepository.RemoveRangeAsync(existingMedia);
                }

                if (command.Media != null && command.Media.Any())
                {
                    foreach (var mediaDto in command.Media)
                    {
                        review.Medias.Add(new ReviewMedia
                        {
                            Url = mediaDto.Url,
                            Type = mediaDto.Type
                        });
                    }
                }

                await _reviewRepository.UpdateAsync(review);
                await _reviewRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating review {ReviewId}", command.ReviewId);
                return false;
            }
        }

        public async Task<bool> HasUserMarkedReviewHelpfulAsync(int reviewId, string userId)
        {
            var hasMarked = await _reviewFeedbackRepository.HasUserMarkedFeedbackAsync(reviewId, userId);

            _logger.LogInfo("User {UserId} has{Marked} marked review {ReviewId} as helpful",
                userId,
                hasMarked ? "" : " not",
                reviewId);

            return hasMarked;
        }

        public async Task<bool> MarkReviewHelpfulAsync(ReviewFeedbackCommand command)
        {
            await using var transaction = await _reviewRepository.BeginTransactionAsync();

            try
            {
                var review = await _reviewRepository.GetReviewWithDetailsAsync(command.ReviewId);

                if (review == null)
                    return false;

                var existingFeedback = await _reviewFeedbackRepository.GetFeedbackByReviewAndUserAsync(command.ReviewId, command.UserId);
                if (existingFeedback != null)
                {
                    // Already marked, no changes allowed
                    return false;
                }

                var newFeedback = command.Adapt<ReviewFeedback>();
                newFeedback.CreatedAt = DateTime.UtcNow;
                review.Feedbacks.Add(newFeedback);

                if (command.IsHelpful)
                    review.HelpfulCount++;
                else
                    review.UnhelpfulCount++;

                await _reviewRepository.UpdateAsync(review);
                await _reviewRepository.SaveChangesAsync();

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                _logger.LogError(ex, "Error updating helpful feedback for review {ReviewId}", command.ReviewId);
                return false;
            }
        }

        public async Task<bool> ReportReviewAsync(ReviewReportCommand command)
        {
            try
            {
                var review = await GetEntityByIdAsync(command.ReviewId);
                if (review == null)
                    return false;

                var report = command.Adapt<ReviewReport>();
                report.ReportedAt = DateTime.UtcNow;
                report.Status = ReportStatus.Pending;

                await _reportRepository.AddAsync(report);
                await _reportRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error reporting review {ReviewId}", command.ReviewId);
                return false;
            }
        }

        public async Task<bool> SupportResolveReportAsync(SupportResolveReportCommand command)
        {
            try
            {
                var report = await _reportRepository.GetByIdAsync(command.ReportId);
                if (report == null || report.ReviewId != command.ReviewId)
                    return false;

                report.Status = ReportStatus.Resolved;
                report.ResolutionNote = command.ResolutionNote;
                report.ResolvedAt = DateTime.UtcNow;
                report.ResolvedByUserId = command.SupportUserId;

                await _reportRepository.UpdateAsync(report);
                await _reportRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resolving report {ReportId}", command.ReportId);
                return false;
            }
        }

        public async Task<bool> ModerateReviewAsync(ReviewModerationCommand command)
        {
            try
            {
                var review = await GetEntityByIdAsync(command.ReviewId);
                if (review == null)
                    return false;

                review.Status = command.Status;
                review.ModerationComment = command.ModerationComment;
                review.ModeratedByUserId = command.ModeratorUserId;
                review.ModeratedAt = DateTime.UtcNow;

                await _reviewRepository.UpdateAsync(review);

                var reports = await _reportRepository.GetPendingReportsByReviewIdAsync(command.ReviewId);
                foreach (var report in reports)
                {
                    report.Status = ReportStatus.Resolved;
                    report.ResolvedAt = DateTime.UtcNow;
                    report.ResolvedByUserId = command.ModeratorUserId;

                    await _reportRepository.UpdateAsync(report);
                }

                await _reportRepository.SaveChangesAsync();
                await _reviewRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in admin moderation of review {ReviewId}", command.ReviewId);
                return false;
            }
        }

        public async Task<ReviewSummaryDto> GetProductReviewSummaryAsync(GetProductReviewSummaryQuery query)
        {
            try
            {
                var reviews = await _reviewRepository.GetActiveReviewsByProductIdAsync(query.ProductId);

                var totalReviews = reviews.Count;
                var averageRating = totalReviews == 0 ? 0 : reviews.Average(r => r.Rating);
                var ratingsBreakdown = reviews.GroupBy(r => r.Rating)
                                             .ToDictionary(g => g.Key, g => g.Count());

                return new ReviewSummaryDto
                {
                    ProductId = query.ProductId,
                    TotalReviews = totalReviews,
                    AverageRating = averageRating,
                    RatingsBreakdown = ratingsBreakdown
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting review summary for product {ProductId}", query.ProductId);
                return new ReviewSummaryDto
                {
                    ProductId = query.ProductId,
                    TotalReviews = 0,
                    AverageRating = 0,
                    RatingsBreakdown = new Dictionary<int, int>()
                };
            }
        }

        public async Task<List<ReviewSummaryDto>> GetProductReviewSummariesAsync(List<int> productIds)
        {
            var summaries = new List<ReviewSummaryDto>();

            try
            {
                var reviews = await _reviewRepository.GetActiveReviewsByProductIdsAsync(productIds);

                var reviewsByProduct = reviews
                    .GroupBy(r => r.ProductId)
                    .ToDictionary(g => g.Key, g => g.ToList());

                foreach (var productId in productIds)
                {
                    if (reviewsByProduct.TryGetValue(productId, out var productReviews))
                    {
                        var totalReviews = productReviews.Count;
                        var averageRating = totalReviews == 0 ? 0 : productReviews.Average(r => r.Rating);
                        var ratingsBreakdown = productReviews
                            .GroupBy(r => r.Rating)
                            .ToDictionary(g => g.Key, g => g.Count());

                        summaries.Add(new ReviewSummaryDto
                        {
                            ProductId = productId,
                            TotalReviews = totalReviews,
                            AverageRating = averageRating,
                            RatingsBreakdown = ratingsBreakdown
                        });
                    }
                    else
                    {
                        // No reviews for this product
                        summaries.Add(new ReviewSummaryDto
                        {
                            ProductId = productId,
                            TotalReviews = 0,
                            AverageRating = 0,
                            RatingsBreakdown = new Dictionary<int, int>()
                        });
                    }
                }

                return summaries;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting review summaries for products");

                // Return default summaries for all products in case of failure
                return productIds.Select(id => new ReviewSummaryDto
                {
                    ProductId = id,
                    TotalReviews = 0,
                    AverageRating = 0,
                    RatingsBreakdown = new Dictionary<int, int>()
                }).ToList();
            }
        }

        public async Task<(List<ReviewDto> Items, int TotalCount)> GetReviewsByProductAsync(GetReviewsByProductQuery query)
        {
            try
            {
                var pagedReviews = await _reviewRepository.GetPaginatedByProductIdAsync(query.ProductId, query.Page, query.PageSize);

                if (pagedReviews.TotalCount == 0)
                    return (new List<ReviewDto>(), 0);

                var dtos = ReviewDto.ToResponseDtoList(pagedReviews.Items, userProvider.UserId ?? "user123");
                return (dtos, pagedReviews.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting reviews by product {ProductId}", query.ProductId);
                return (new List<ReviewDto>(), 0);
            }
        }

        public async Task<(List<ReviewDto> Items, int TotalCount)> GetReviewsByUserAsync(GetReviewsByUserQuery query)
        {
            try
            {
                var pagedReviews = await _reviewRepository.GetPaginatedByUserIdAsync(query.UserId, query.Page, query.PageSize);

                if (pagedReviews.TotalCount == 0)
                    return (new List<ReviewDto>(), 0);

                var dtos = pagedReviews.Items.Adapt<List<ReviewDto>>();
                return (dtos, pagedReviews.TotalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting reviews by user {UserId}", query.UserId);
                return (new List<ReviewDto>(), 0);
            }
        }

        public async Task<bool> DeleteAsync(ReviewDto reviewDto)
        {
            if (reviewDto == null)
                return false;

            try
            {
                var review = await _reviewRepository.GetByIdAsync(reviewDto.Id);
                if (review == null)
                    return false;

                await _reviewRepository.DeleteAsync(review);
                await _reviewRepository.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting review {ReviewId}", reviewDto.Id);
                return false;
            }
        }

        public async Task<(List<ReportedReviewDto> Items, int TotalCount)> GetReportedReviewsAsync(int page, int pageSize)
        {
            try
            {
                var (reports, totalCount) = await _reportRepository.GetPendingReportsAsync(page, pageSize);

                if (reports == null || reports.Count == 0)
                    return (new List<ReportedReviewDto>(), 0);

                var dtos = reports.Select(r => new ReportedReviewDto
                {
                    ReportId = r.Id,
                    ReviewId = r.ReviewId,
                    ReportedByUserId = r.ReportedByUserId,
                    Reason = r.Reason,
                    ReportedAt = r.ReportedAt,
                    Status = r.Status
                }).ToList();

                return (dtos, totalCount);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error fetching reported reviews");
                return (new List<ReportedReviewDto>(), 0);
            }
        }
    }

}