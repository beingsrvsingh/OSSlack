using System.ComponentModel.DataAnnotations;

namespace Catalog.Domain.Entities;

public partial class CategoryMaster
{
    [Key]
    public int Id { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; } = null!;

    [MaxLength(500)]
    public string? Description { get; set; }

    public int DisplayOrder { get; set; }

    [MaxLength(300)]
    public string? ImageUrl { get; set; }

    public bool IsActive { get; set; } = true;
    public virtual ICollection<SubCategoryMaster> SubCategoryMasters { get; set; } = new List<SubCategoryMaster>();
    public ICollection<CategoryLocalizedText> Localizations { get; set; } = new List<CategoryLocalizedText>();

}
