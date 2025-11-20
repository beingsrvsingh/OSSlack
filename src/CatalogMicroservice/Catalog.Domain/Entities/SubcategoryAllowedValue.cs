namespace Catalog.Domain.Entities
{
    public class SubcategoryAllowedValue
    {
        public int Id { get; set; }

        public int SubCategoryId { get; set; }

        public int AttributeId { get; set; }

        public int AllowedValueId { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual SubCategoryMaster SubCategoryMaster { get; set; }

        public virtual CatalogAttribute Attribute { get; set; }

        public virtual CatalogAttributeAllowedValue CatalogAttributeAllowedValue { get; set; }
    }
}
