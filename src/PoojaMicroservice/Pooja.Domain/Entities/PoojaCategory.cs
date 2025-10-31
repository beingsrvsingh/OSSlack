namespace Pooja.Domain.Entities
{
    public class PoojaCategory
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public ICollection<PoojaMaster> PoojaMasters { get; set; } = new List<PoojaMaster>();
    }

}
