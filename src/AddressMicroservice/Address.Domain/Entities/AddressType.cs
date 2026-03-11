
namespace Address.Domain.Entities
{
    public class AddressType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string DisplayName { get; set; } = null!;
        public string? Description { get; set; }
        public ICollection<AddressEntity> Addresses { get; set; } = new List<AddressEntity>();

    }

}