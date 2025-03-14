namespace Catalog.Domain.Entities;

public partial class CategoryMaster
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<SubCategoryMaster> SubCategoryMasters { get; set; } = new List<SubCategoryMaster>();
}
