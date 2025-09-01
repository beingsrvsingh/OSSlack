
namespace Product.Application.Contracts
{
    public class ReviewSummaryDto
    {
        public int ProductId { get; set; }
        public double AverageRating { get; set; }
        public int TotalReviews { get; set; }

        public Dictionary<int, int> RatingsBreakdown { get; set; } = []; // e.g. 5★ = 120
    }
}