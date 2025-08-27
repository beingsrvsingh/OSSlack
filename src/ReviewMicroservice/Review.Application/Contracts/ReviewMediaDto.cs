using Review.Domain;

namespace Review.Application.Contracts
{
    public class ReviewMediaDto
    {
        public string Url { get; set; } = string.Empty;
        public MediaType Type { get; set; }
    }

}