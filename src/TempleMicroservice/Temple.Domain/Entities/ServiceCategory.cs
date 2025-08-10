using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public class ServiceCategory
    {
        public int Id { get; set; }

        public int ServicePackageId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        public string? Description { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public bool IsActive { get; set; } = true;

        public ServicePackage ServicePackage { get; set; } = null!;
    }
}