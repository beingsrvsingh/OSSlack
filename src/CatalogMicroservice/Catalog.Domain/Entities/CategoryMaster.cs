using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public partial class CategoryMaster
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required, MaxLength(100)]
    public string CategoryType { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    [MaxLength(300)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<SubCategoryMaster> SubCategoryMasters { get; set; } = new List<SubCategoryMaster>();    
    public virtual ICollection<CategoryLocalizedText> Localizations { get; set; } = new List<CategoryLocalizedText>();
}

