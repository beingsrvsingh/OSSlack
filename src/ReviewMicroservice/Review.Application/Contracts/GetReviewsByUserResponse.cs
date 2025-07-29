

namespace Review.Application.Contracts
{
    public class GetReviewsByUserResponse
    {
        public List<ReviewDto> Reviews { get; set; } = new();
        public int TotalCount { get; set; }
    }

}