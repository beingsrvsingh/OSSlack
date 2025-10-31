namespace Pooja.Domain.Entities
{
    public class PoojaAddon
    {
        public int Id { get; set; }
        public int PoojaId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsOptional { get; set; } = true;

        public PoojaMaster PoojaMaster { get; set; } = null!;
    }

}
