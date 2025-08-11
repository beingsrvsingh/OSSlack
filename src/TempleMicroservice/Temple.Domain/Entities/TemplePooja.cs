using System.ComponentModel.DataAnnotations;

namespace Temple.Domain.Entities
{
    public partial class TemplePooja
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int TempleId { get; set; }

        [Required]
        public int PoojaMasterId { get; set; }  // FK to Catalog MS's PoojaMaster

        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        // Navigation property
        public virtual TempleMaster TempleMaster { get; set; } = null!;
    }

}