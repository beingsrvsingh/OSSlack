namespace Pooja.Domain.Entities
{
    public class PoojaTemple
    {
        public int Id { get; set; }
        public int PoojaId { get; set; }
        public int TempleId { get; set; } // Reference to Temple Microservice

        public decimal TempleCharges { get; set; }
        public bool PriestIncluded { get; set; } // Overrides Pooja default
        public bool IsAvailable { get; set; } = true;

        public PoojaMaster PoojaMaster { get; set; } = null!;
    }

}
