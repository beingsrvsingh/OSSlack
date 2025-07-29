namespace Review.Domain.Entities
{
    public partial class ReviewMedia
    {
        public int Id { get; set; }
        public int ReviewId { get; set; }
        public string Url { get; set; } = null!;
        public MediaType Type { get; set; } = MediaType.Image;
        public Review Review { get; set; } = null!;
    }
}
