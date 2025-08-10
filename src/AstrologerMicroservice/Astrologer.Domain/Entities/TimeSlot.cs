namespace AstrologerMicroservice.Domain.Entities
{
    public partial class TimeSlot
    {
        public int Id { get; set; }
        public int AstrologerId { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public bool IsBooked { get; set; }

        public virtual Astrologer Astrologer { get; set; } = null!;
    }

}