using Review.Domain.Entities;
using Review.Domain.Enum;

namespace Review.Application.Contracts
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string UserId { get; set; } = null!;
        public int Rating { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public List<ReviewMedia> Media { get; set; } = new();
        public int HelpfulCount { get; set; }
        public int UnhelpfulCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public ReviewStatus Status { get; set; }
        public string? ModerationComment { get; set; }
        public string? ModeratedByUserId { get; set; }
        public DateTime? ModeratedAt { get; set; }
    }
}