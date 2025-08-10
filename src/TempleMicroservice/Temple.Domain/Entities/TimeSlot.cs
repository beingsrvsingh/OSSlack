
namespace Temple.Domain.Entities
{
    public partial class TimeSlot
    {
        public int Id { get; set; }
        public int TempleId { get; set; }

        public DateTime StartUtc { get; set; }
        public DateTime EndUtc { get; set; }

        public bool IsBooked { get; set; }

        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }

}