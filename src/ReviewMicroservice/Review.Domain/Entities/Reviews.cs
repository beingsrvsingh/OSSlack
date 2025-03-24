namespace Review.Domain.Entities;

public partial class Reviews
{
    public required int Id { get; set; }

    public required string UserId { get; set; }

    public required int ProductId { get; set; }

    public required String UserName { get; set; }

    public int Star { get; set; }

    public string Title { get; set; } = null!;

    public string? ShortDescription { get; set; }

    public DateTime CreatedDate { get; set; }

    public bool IsReviewed { get; set; }

    public string? ReviewedBy { get; set; }

    public DateTime? ReviewedDate { get; set; }

    public string? ReviewReason { get; set; }

    public virtual ICollection<ReviewDetail> ReviewDetails { get; set; } = new List<ReviewDetail>();
}
