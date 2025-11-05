using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shared.Domain.Entities.Base
{
    public partial class BaseAddon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = null!;

        [MaxLength(300)]
        public string? Description { get; set; }

        public decimal Price { get; set; }

        [MaxLength(50)]
        public string? Currency { get; set; } = "INR";

        public bool IsActive { get; set; } = true;

        public int DisplayOrder { get; set; } = 0;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
