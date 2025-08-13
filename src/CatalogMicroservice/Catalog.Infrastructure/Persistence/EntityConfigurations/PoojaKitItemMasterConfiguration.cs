using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaKitItemMasterConfiguration : IEntityTypeConfiguration<PoojaKitItemMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaKitItemMaster> builder)
        {
            builder.ToTable("pooja_kit_item_Master");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.KitSubcategoryId).HasColumnName("kit_subcategory_id");
            builder.Property(p => p.ProductSubcategoryId).HasColumnName("product_subcategory_id");
            builder.Property(p => p.Notes).HasColumnName("notes").HasMaxLength(500);

            builder.HasOne(p => p.KitSubcategory)
                   .WithMany()
                   .HasForeignKey(p => p.KitSubcategoryId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.ProductSubcategory)
                   .WithMany()
                   .HasForeignKey(p => p.ProductSubcategoryId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}