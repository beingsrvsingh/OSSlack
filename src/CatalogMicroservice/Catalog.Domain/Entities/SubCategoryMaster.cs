using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.Domain.Entities;

public partial class SubCategoryMaster
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CategoryMasterId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    [ForeignKey("CategoryMasterId")]
    public virtual CategoryMaster CategoryMaster { get; set; } = null!;

    public virtual ICollection<PoojaKitMaster> PoojaKits { get; set; } = new List<PoojaKitMaster>();
    public ICollection<SubCategoryLocalizedText> Localizations { get; set; } = new List<SubCategoryLocalizedText>();
    public virtual ICollection<PoojaMaster> PoojaMasters { get; set; } = new List<PoojaMaster>();
}
