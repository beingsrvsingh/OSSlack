
using Review.Domain;

namespace Review.Application.Contracts
{
    public class MediaDto
    {
        public string Url { get; set; } = string.Empty;
        public MediaType Type { get; set; } = MediaType.Image;
    }
}