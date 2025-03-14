namespace Catalog.Domain.Entities;

public partial class ChildSubCategoryMaster
{
    public int Id { get; set; }

    public int SubCategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual SubCategoryMaster SubCategory { get; set; } = null!;
}
