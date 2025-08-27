using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Catalog.Domain.Enums;

namespace Catalog.Domain.Entities;

public partial class SubCategoryMaster
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int CategoryMasterId { get; set; }

    [MaxLength(100)]
    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; } = true;

    [Required]
    public SubcategoryType SubcategoryType { get; set; }

    public int? ParentSubcategoryId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    
    public virtual CategoryMaster CategoryMaster { get; set; } = null!;

    [ForeignKey(nameof(ParentSubcategoryId))]
    public virtual SubCategoryMaster? ParentSubcategory { get; set; }

    public virtual ICollection<SubCategoryMaster> ChildSubcategories { get; set; } = new List<SubCategoryMaster>();
    public virtual ICollection<CatalogAttribute> CatalogAttributes { get; set; } = new List<CatalogAttribute>();
    public virtual ICollection<SubCategoryLocalizedText> Localizations { get; set; } = new List<SubCategoryLocalizedText>();
}

