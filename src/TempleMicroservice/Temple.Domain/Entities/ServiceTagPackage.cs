using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public class ServiceTagPackage
    {
        [Key]
        public int Id { get; set; }

        public int ServicePackageId { get; set; }
        public int TagId { get; set; }

        public ServicePackage ServicePackage { get; set; } = null!;
        public ServiceTagPackageMaster Tag { get; set; } = null!;
    }
}