namespace Review.Domain.Entities
{
    public partial class ReviewDetail
    {
        public int Id { get; set; }

        public int ReviewId { get; set; }

        public int? ReviewReportLookUpid { get; set; }

        public string UserId { get; set; } = null!;

        public int ProductId { get; set; }

        public string? Message { get; set; }

        public bool IsHelpFul { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? ModiFiedDate { get; set; }

        public virtual Reviews Review { get; set; } = null!;

        public virtual ReviewReportLookup ReviewReportLookUp { get; set; } = null!;
    }
}
