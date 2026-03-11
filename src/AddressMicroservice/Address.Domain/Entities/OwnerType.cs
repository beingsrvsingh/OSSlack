namespace Address.Domain.Entities
{
    public class OwnerType
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<AddressEntity>? Addresses { get; set; }
    }
}
