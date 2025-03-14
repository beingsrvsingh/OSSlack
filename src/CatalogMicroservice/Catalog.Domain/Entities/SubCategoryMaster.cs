namespace Catalog.Domain.Entities;

public partial class SubCategoryMaster
{
    public int Id { get; set; }

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public int DisplayOrder { get; set; }

    public bool IsActive { get; set; }

    public virtual CategoryMaster Category { get; set; } = null!;

    public virtual ICollection<ChildSubCategoryMaster> ChildSubCategoryMasters { get; set; } = new List<ChildSubCategoryMaster>();
}
