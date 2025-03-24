namespace Review.Domain.Entities
{
    public partial class ReviewDetail
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public int? ReviewReportLookUpId { get; set; }

        public required string UserId { get; set; }

        public required String UserName { get; set; }

        public required int ProductId { get; set; }

        public string? Message { get; set; }

        public bool IsHelpful { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        public virtual Reviews Review { get; set; } = null!;

        public virtual ReviewReportLookup ReviewReportLookUp { get; set; } = null!;
    }
}
