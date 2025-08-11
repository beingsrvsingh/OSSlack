using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    public class PoojaKitMasterConfiguration : IEntityTypeConfiguration<PoojaKitMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaKitMaster> builder)
        {
            builder.ToTable("pooja_kit_master");

            builder.HasKey(pk => pk.Id);

            builder.Property(pk => pk.Id).HasColumnName("id");

            builder.Property(pk => pk.SubCategoryMasterId)
                .IsRequired()
                .HasColumnName("sub_category_master_id");

            builder.Property(pk => pk.TempleId)
                .IsRequired()
                .HasColumnName("temple_id");

            builder.Property(pk => pk.Name)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnName("name");

            builder.Property(pk => pk.Description)
                .HasColumnName("description");

            builder.Property(pk => pk.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");

            builder.Property(pk => pk.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP(6)")
                .HasColumnName("created_at");

            builder.Property(pk => pk.UpdatedAt)
                .HasColumnName("updated_at");

            builder.HasOne(pk => pk.SubCategoryMaster)
                .WithMany(sc => sc.PoojaKits)
                .HasForeignKey(pk => pk.SubCategoryMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pk => pk.PoojaKitItems)
                   .WithOne(pki => pki.PoojaKitMaster)
                   .HasForeignKey(pki => pki.PoojaKitMasterId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(pk => pk.Localizations)
                   .WithOne(l => l.PoojaKit)
                   .HasForeignKey(l => l.PoojaKitId)
                   .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(pk => pk.PoojaMaster)
                    .WithMany(p => p.PoojaKits)
                    .HasForeignKey(pk => pk.PoojaMasterId)
                    .OnDelete(DeleteBehavior.Cascade);

        }
    }

}