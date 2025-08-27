using Review.Domain.Entities;

namespace Review.Application.Contracts
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int Pid { get; set; }
        public string Uid { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = "Anonymous";
        public int Rating { get; set; }

        // Nullable so we know if user hasn't interacted
        public bool? IsHelpful { get; set; }

        public bool IsMyReview { get; set; }

        public DateTime ReviewDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<string> MediaDto { get; set; } = new();
        public int HelpfulCount { get; set; }

        // Modified to accept currentUserId
        public static ReviewDto ToResponseDto(Review.Domain.Entities.Review review, string currentUserId)
        {
            var userFeedback = review.Feedbacks.FirstOrDefault(f => f.UserId == currentUserId);

            return new ReviewDto
            {
                Id = review.Id,
                Pid = review.ProductId,
                Uid = review.UserId,
                CreatedBy = review.CreatedBy,
                Rating = review.Rating,
                ReviewDate = review.CreatedAt,
                Title = review.Title,
                Comment = review.Comment,
                HelpfulCount = review.HelpfulCount,
                MediaDto = review.Medias.Select(m => m.Url).ToList(),

                // IsHelpful is nullable: true/false/null
                IsHelpful = userFeedback?.IsHelpful,

                // Check if this review was written by the current user
                IsMyReview = review.UserId == currentUserId
            };
        }

        // Also update list conversion to accept user context
        public static List<ReviewDto> ToResponseDtoList(IEnumerable<Review.Domain.Entities.Review> reviews, string currentUserId)
        {
            return reviews.Select(review => ToResponseDto(review, currentUserId)).ToList();
        }
    }

}