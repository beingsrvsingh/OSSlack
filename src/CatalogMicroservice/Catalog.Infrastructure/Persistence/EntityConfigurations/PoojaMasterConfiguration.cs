using Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Catalog.Infrastructure.Persistence.EntityConfigurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PoojaMasterConfiguration : IEntityTypeConfiguration<PoojaMaster>
    {
        public void Configure(EntityTypeBuilder<PoojaMaster> builder)
        {
            builder.ToTable("pooja_master");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasMaxLength(1000);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(300);

            builder.Property(p => p.IsActive)
                .HasDefaultValue(true);

            builder.Property(p => p.IsComposite)
                .HasDefaultValue(false);

            builder.Property(p => p.BasePrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.DisplayOrder)
                .HasDefaultValue(0);

            builder.Property(p => p.CreatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            builder.HasOne(p => p.SubCategoryMaster)
                .WithMany(s => s.PoojaMasters)
                .HasForeignKey(p => p.SubCategoryMasterId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Localizations)
                .WithOne(l => l.PoojaMaster)
                .HasForeignKey(l => l.PoojaMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.PoojaKits)
                .WithOne(k => k.PoojaMaster)
                .HasForeignKey(k => k.PoojaMasterId)
                .OnDelete(DeleteBehavior.Cascade);

            // Many-to-many with Tags via join table PoojaMasterTags
            builder
                .HasMany(p => p.Tags)
                .WithMany(t => t.Poojas)
                .UsingEntity<Dictionary<string, object>>(
                    "PoojaMasterTag",
                    j => j
                        .HasOne<PoojaTag>()
                        .WithMany()
                        .HasForeignKey("TagId")
                        .HasConstraintName("FK_PoojaMasterTag_TagId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<PoojaMaster>()
                        .WithMany()
                        .HasForeignKey("PoojaMasterId")
                        .HasConstraintName("FK_PoojaMasterTag_PoojaMasterId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("PoojaMasterId", "TagId");
                        j.ToTable("PoojaMasterTag");
                    }
                );
        }
    }

}