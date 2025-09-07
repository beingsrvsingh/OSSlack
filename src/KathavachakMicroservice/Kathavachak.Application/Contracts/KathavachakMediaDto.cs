namespace Kathavachak.Application.Contracts
{
    public class KathavachakMediaDto
    {
        public int Id { get; set; }
        public int KathavachakId { get; set; }
        public string Url { get; set; } = null!;
        public string MediaType { get; set; } = null!;  // e.g., "video", "audio", "image"
    }
}
