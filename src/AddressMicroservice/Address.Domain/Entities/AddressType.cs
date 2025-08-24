
namespace Address.Domain.Entities
{
    public class AddressType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;  // e.g., Home, Work, Temple, etc.
        public string? Description { get; set; }
        public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();

    }

}