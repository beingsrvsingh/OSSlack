
namespace Review.Application.Contracts
{
    public class ReviewReportReasonDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? Descriptions { get; set; }
        public int DisplayOrder { get; set; }
    }
}