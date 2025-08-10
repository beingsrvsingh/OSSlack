using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public class ServiceTagPackageMaster
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public ICollection<ServiceTagPackage> serviceTags { get; set; } = new List<ServiceTagPackage>();
    }

}