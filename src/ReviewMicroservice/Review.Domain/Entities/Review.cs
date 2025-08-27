using Review.Domain.Enum;

namespace Review.Domain.Entities;

public partial class Review
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string UserId { get; set; } = null!;    
    public int Rating { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Comment { get; set; } = string.Empty;

    // Aggregate counts for performance
    public int HelpfulCount { get; set; }
    public int UnhelpfulCount { get; set; }

    public DateTime CreatedAt { get; set; }
    public string CreatedBy { get; set; } = "Anonymous";
    public DateTime? UpdatedAt { get; set; }

    // Current moderation status (summary)
    public ReviewStatus Status { get; set; }
    public string? ModerationComment { get; set; }
    public string? ModeratedByUserId { get; set; }
    public DateTime? ModeratedAt { get; set; }

    // Navigation properties for detailed feedback and moderation records
    public ICollection<ReviewFeedback> Feedbacks { get; set; } = new List<ReviewFeedback>();
    public ICollection<ReviewMedia> Medias { get; set; } = new List<ReviewMedia>();
    public ICollection<ReviewReport> Reports { get; set; } = new List<ReviewReport>();
}
