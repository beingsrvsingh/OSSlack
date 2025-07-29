
namespace Review.Domain.Entities
{
    public partial class ReviewFeedback
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string UserId { get; set; } = null!; // Who gave the feedback
        public bool IsHelpful { get; set; } // true if helpful, false if unhelpful
        public DateTime CreatedAt { get; set; }
        public Review Review { get; set; } = null!; // Navigation back to Review
    }
}