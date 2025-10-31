namespace Pooja.Domain.Entities
{
    public class PoojaPriest
    {
        public int Id { get; set; }
        public int PoojaId { get; set; }
        public int PriestId { get; set; } // Reference to Priest Microservice

        public bool IsPreferred { get; set; } // High-rated or recommended priest
        public decimal? CustomCharges { get; set; } // If priest charges extra
        public bool IsAvailable { get; set; } = true;

        public PoojaMaster PoojaMaster { get; set; } = null!;
    }

}
