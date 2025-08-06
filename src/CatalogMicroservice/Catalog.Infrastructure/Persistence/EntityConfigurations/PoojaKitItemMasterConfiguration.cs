using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaKitItemMasterConfiguration : IEntityTypeConfiguration<PoojaKitItemMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaKitItemMaster> builder)
        {
            builder.ToTable("pooja_kit_item_master");

            builder.HasKey(pki => pki.Id);

            builder.Property(pki => pki.Id).HasColumnName("id");

            builder.Property(pki => pki.PoojaKitMasterId)
                .IsRequired()
                .HasColumnName("pooja_kit_master_id");

            builder.Property(pki => pki.ItemName)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("item_name");

            builder.Property(pki => pki.Description)
                .HasColumnName("description");

            builder.Property(pki => pki.Quantity)
                .HasDefaultValue(1)
                .HasColumnName("quantity");

            builder.Property(pki => pki.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(pki => pki.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()")
                .HasColumnName("created_at");

            builder.Property(pki => pki.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasOne(pki => pki.PoojaKitMaster)
                .WithMany(pk => pk.PoojaKitItems)
                .HasForeignKey(pki => pki.PoojaKitMasterId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}