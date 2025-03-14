namespace Review.Domain.Entities
{
    public partial class ReviewReportLookup
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string DisplayName { get; set; } = null!;

        public string? Descriptions { get; set; }

        public int DisplayOrder { get; set; }

        public virtual ICollection<ReviewDetail> ReviewDetails { get; set; } = new List<ReviewDetail>();
    }
}
